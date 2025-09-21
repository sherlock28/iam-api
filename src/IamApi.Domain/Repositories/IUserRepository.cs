using IamApi.Domain.Entities;

namespace IamApi.Domain.Repositories;

public interface IUserRepository
{
	Task AddAsync(User user, CancellationToken cancellationToken = default);
	Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
