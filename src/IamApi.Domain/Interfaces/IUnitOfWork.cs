using IamApi.Domain.Repositories;

namespace IamApi.Domain.Interfaces;

public interface IUnitOfWork
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	Task BeginTransactionAsync(CancellationToken cancellationToken = default);
	Task CommitTransactionAsync(CancellationToken cancellationToken = default);
	Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

	// Specific repositories
	IUserRepository UsersRepository { get; }
	IOrganizationRepository OrganizationRepository { get; }
}
