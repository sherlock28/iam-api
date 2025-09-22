using IamApi.Domain.Entities;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IamApi.Infrastructure.Repositories;

internal class OrganizationRepository(IAMDbContext context) : IOrganizationRepository
{
	private readonly IAMDbContext _context = context;

	public async Task AddAsync(Organization org, CancellationToken cancellationToken = default)
	{
		await _context.Organizations.AddAsync(org, cancellationToken);
	}

	public async Task<Organization?> GetOrganizationByIdAsync(Guid organizationId, CancellationToken cancellationToken = default)
	{
		var org = await _context.Organizations
			.FirstOrDefaultAsync(o => o.Id == organizationId, cancellationToken);

		return org;
	}
}
