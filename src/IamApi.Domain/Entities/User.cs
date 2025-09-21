using IamApi.Domain.Intefaces;

namespace IamApi.Domain.Entities;

public class User : BaseEntity, ISoftDelete, IMultiTenant
{
	public Guid OrganizationId { get; set; }
	public Organization Organization { get; set; } = default!;

	public Guid? ServiceId { get; set; }

	public string Email { get; set; } = default!;
	public string NormalizedEmail { get; set; } = default!;
	public string Username { get; set; } = default!;
	public string NormalizedUsername { get; set; } = default!;
	public string PasswordHash { get; set; } = default!;
	public DateTime LockoutEnd { get; set; } = default!;
	public bool LockoutEnabled { get; set; } = false;
	public int AccessFailedCount { get; set; } = default!;

	public bool IsActive { get; set; } = true;
	public bool IsDeleted { get; set; } = false;

	public List<UserRole> UserRoles { get; set; } = [];
	public List<UserService> UserServices { get; set; } = [];
}
