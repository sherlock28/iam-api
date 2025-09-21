using IamApi.Domain.Entities;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IamApi.Infrastructure.Repositories;

internal class UserRepository(IAMDbContext context) : IUserRepository
{
	private readonly IAMDbContext _context = context;

	public async Task AddAsync(User user)
	{
		await _context.Users.AddAsync(user);
	}

	public async Task<User?> GetUserByIdAsync(Guid userId)
	{
		var user = await _context.Users
			.FirstOrDefaultAsync(u => u.Id == userId);

		return user;
	}
}
