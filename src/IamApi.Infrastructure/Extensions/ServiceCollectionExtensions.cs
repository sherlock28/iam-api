using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IamApi.Domain.Interfaces;
using IamApi.Domain.Repositories;
using IamApi.Infrastructure.Persistence;
using IamApi.Infrastructure.Repositories;

namespace IamApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("IAMDB");
		services.AddDbContext<IAMDbContext>(options => options.UseNpgsql(connectionString, pgOptions => pgOptions.MigrationsAssembly("IamApi.Infrastructure"))
		  .EnableSensitiveDataLogging()
		);

		// Repositories
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
