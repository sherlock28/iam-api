using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore; 
using IamApi.Domain.Entities;
using IamApi.Domain.Interfaces;

namespace IamApi.Infrastructure.Persistence;

public class IAMDbContext : DbContext
{
	private readonly ITenantProvider? _tenantProvider;
	private readonly bool _isMigration;

	public DbSet<Organization> Organizations { get; set; }
	public DbSet<Permission> Permissions { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<RolePermission> RolePermissions { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<UserRole> UserRoles { get; set; }
	public DbSet<UserService> UserServices { get; set; }
	public DbSet<UsrApplication> UsrApplications { get; set; }
	public DbSet<UsrApplicationRole> UsrApplicationRoles { get; set; }

	public IAMDbContext(DbContextOptions<IAMDbContext> options, ITenantProvider? tenantProvider = null) : base(options)
    {
		_tenantProvider = tenantProvider;
		_isMigration = tenantProvider == null;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// ============ Organization Relations ============
		// Organization -> Application (One-to-Many)
		modelBuilder.Entity<Organization>()
			.HasMany(o => o.OwnedUsrApplications)
			.WithOne(a => a.Organization)
			.HasForeignKey(a => a.OrganizationId);

		// Organization -> User (One-to-Many)
		modelBuilder.Entity<Organization>()
			.HasMany(o => o.OwnedUsers)
			.WithOne(u => u.Organization)
			.HasForeignKey(u => u.OrganizationId);

		// Organization -> Role (One-to-Many)
		modelBuilder.Entity<Organization>()
			.HasMany(o => o.OwnedRoles)
			.WithOne(r => r.Organization)
			.HasForeignKey(r => r.OrganizationId);

		// Organization -> Permission (One-to-Many)
		modelBuilder.Entity<Organization>()
			.HasMany(o => o.OwnedPermissions)
			.WithOne(p => p.Organization)
			.HasForeignKey(p => p.OrganizationId);


		// ============ User Relations ============
		// User -> UserRole (One-to-Many)
		modelBuilder.Entity<User>()
			.HasMany(u => u.UserRoles)
			.WithOne(ur => ur.User)
			.HasForeignKey(ur => ur.UserId);

		// User -> UserService (One-to-Many)
		modelBuilder.Entity<User>()
			.HasMany(u => u.UserServices)
			.WithOne(us => us.User)
			.HasForeignKey(us => us.UserId);


		// ============ Application Relations ============
		// Application -> ApplicationRole (One-to-Many)
		modelBuilder.Entity<UsrApplication>()
			.HasMany(a => a.ApplicationRoles)
			.WithOne(ar => ar.UsrApplication)
			.HasForeignKey(ar => ar.UsrApplicationId);

		// ============ Role Relations ============
		// Role -> ApplicationRole (One-to-Many)
		modelBuilder.Entity<Role>()
			.HasMany(r => r.RolePermissions)
			.WithOne(rp => rp.Role)
			.HasForeignKey(rp => rp.RoleId);


		// ============ Indexes for performance ============
		modelBuilder.Entity<User>()
			.HasIndex(u => u.Email)
			.IsUnique();

		modelBuilder.Entity<User>()
			.HasIndex(u => u.Username)
			.IsUnique();

		modelBuilder.Entity<Permission>()
			.HasIndex(p => p.Code)
			.IsUnique();

		if (!_isMigration && _tenantProvider != null)
		{
			var tenantId = _tenantProvider.GetCurrentTenantId() ?? Guid.Empty;

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			if (typeof(IMultiTenant).IsAssignableFrom(entityType.ClrType))
			{
				modelBuilder.Entity(entityType.ClrType).HasQueryFilter(
					BuildTenantFilterExpression(entityType.ClrType, tenantId)
				);
			}

			if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
			{
				modelBuilder.Entity(entityType.ClrType).HasQueryFilter(
					BuildSoftDeleteFilterExpression(entityType.ClrType)
				);
			}
		}
		}
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var userId = Guid.CreateVersion7();

		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.UtcNow;
				entry.Entity.CreatedBy = userId;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Entity.LastModifiedByAt = DateTime.UtcNow;
				entry.Entity.LastModifiedBy = userId;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}

	private static LambdaExpression BuildTenantFilterExpression(Type entityType, Guid tenantId)
	{
		var parameter = Expression.Parameter(entityType, "e");
		var property = Expression.Property(parameter, nameof(IMultiTenant.OrganizationId));
		var tenantConstant = Expression.Constant(tenantId);
		var body = Expression.Equal(property, tenantConstant);
		return Expression.Lambda(body, parameter);
	}

	private static LambdaExpression BuildSoftDeleteFilterExpression(Type entityType)
	{
		var parameter = Expression.Parameter(entityType, "e");
		var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
		var falseConstant = Expression.Constant(false);
		var body = Expression.Equal(property, falseConstant);
		return Expression.Lambda(body, parameter);
	}
}
