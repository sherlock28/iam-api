using IamApi.Domain.Entities;

namespace IamApi.Domain.Repositories;

public interface IAppServiceRepository
{
	Task AddAsync(Service service, CancellationToken cancellationToken = default);
	Task<Service?> GetServiceByIdAsync(Guid serviceId, CancellationToken cancellationToken = default);
}
