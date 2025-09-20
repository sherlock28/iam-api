using Microsoft.EntityFrameworkCore; 
using IamApi.Domain.Entities;

namespace IamApi.Infrastructure.Persistence;

internal class IAMDbContext : DbContext
{
    public IAMDbContext(DbContextOptions<IAMDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
}
