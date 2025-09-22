using IamApi.Domain.Entities;

namespace IamApi.Domain.Repositories;

public interface IOrganizationRepository
{
	Task AddAsync(Organization org, CancellationToken cancellationToken = default);
	Task<Organization?> GetOrganizationByIdAsync(Guid organizationId, CancellationToken cancellationToken = default);
}
