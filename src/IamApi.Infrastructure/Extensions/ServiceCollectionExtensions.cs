using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IamApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("IAMDB");
		services.AddDbContext<IAMDbContext>(options => options.UseNpgsql(connectionString, pgOptions => pgOptions.MigrationsAssembly("IamApi.Infrastructure"))
		  .EnableSensitiveDataLogging()
		);
	}
}
