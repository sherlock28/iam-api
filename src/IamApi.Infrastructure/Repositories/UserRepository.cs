using IamApi.Domain.Entities;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IamApi.Infrastructure.Repositories;

internal class UserRepository(IAMDbContext context) : IUserRepository
{
	private readonly IAMDbContext _context = context;

	public async Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		await _context.Users.AddAsync(user, cancellationToken);
	}

	public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
	{
		var user = await _context.Users
			.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

		return user;
	}
}
