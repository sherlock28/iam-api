using IamApi.Domain.Entities;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IamApi.Infrastructure.Repositories;

internal class RoleRepository(IAMDbContext context) : IRoleRepository
{
	private readonly IAMDbContext _context = context;

	public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
	{
		await _context.Roles.AddAsync(role, cancellationToken);
	}

	public async Task<Role?> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
	{
		var role = await _context.Roles
			.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);

		return role;
	}
}
