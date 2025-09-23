using IamApi.Domain.Entities;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IamApi.Infrastructure.Repositories;

internal class AppServiceRepository(IAMDbContext context) : IAppServiceRepository
{
	private readonly IAMDbContext _context = context;

	public async Task AddAsync(Service service, CancellationToken cancellationToken = default)
	{
		await _context.Services.AddAsync(service, cancellationToken);
	}

	public async Task<Service?> GetServiceByIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
	{
		var service = await _context.Services
			.FirstOrDefaultAsync(o => o.Id == serviceId, cancellationToken);

		return service;
	}
}
