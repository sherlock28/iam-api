using IamApi.Domain.Interfaces;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace IamApi.Infrastructure.Repositories;

internal class UnitOfWork(
	IAMDbContext context,
	IUserRepository usersRepo,
	IOrganizationRepository orgRepo,
	IRoleRepository roleRepo,
	IAppServiceRepository appServiceRepo) : IUnitOfWork
{
	private readonly IAMDbContext _context = context;
	private IDbContextTransaction? _transaction;

	public IUserRepository UsersRepository { get; } = usersRepo;
	public IOrganizationRepository OrganizationRepository { get; } = orgRepo;
	public IRoleRepository RoleRepository { get; } = roleRepo;
	public IAppServiceRepository AppServiceRepository { get; } = appServiceRepo;

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		_transaction = await _context.Database.BeginTransactionAsync();
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await SaveChangesAsync(cancellationToken);
			if (_transaction != null)
			{
				await _transaction.CommitAsync(cancellationToken);
			}
		}
		catch
		{
			await RollbackTransactionAsync(cancellationToken);
			throw;
		}
		finally
		{
			_transaction?.Dispose();
			_transaction = null;
		}
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (_transaction != null)
		{
			await _transaction.RollbackAsync(cancellationToken);
			_transaction.Dispose();
			_transaction = null;
		}
	}

	public void Dispose()
	{
		_transaction?.Dispose();
		_context?.Dispose();
	}
}
