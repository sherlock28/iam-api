using IamApi.Domain.Entities;

namespace IamApi.Domain.Repositories;

public interface IRoleRepository
{
	Task AddAsync(Role role, CancellationToken cancellationToken = default);
	Task<Role?> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}
