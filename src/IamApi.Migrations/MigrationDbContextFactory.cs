using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using IamApi.Infrastructure.Persistence;

namespace IamApi.Migrations;

public class MigrationDbContextFactory : IDesignTimeDbContextFactory<IAMDbContext>
{
	public IAMDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
		   .SetBasePath(Directory.GetCurrentDirectory())
		   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		   .AddEnvironmentVariables()
		   .Build();

		var connectionString = configuration.GetConnectionString("IAMDB");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw new InvalidOperationException("Connection string 'IAMDB' not found.");
		}

		var optionsBuilder = new DbContextOptionsBuilder<IAMDbContext>();

		optionsBuilder.UseNpgsql(connectionString, pgOptions =>
		{
			pgOptions.MigrationsAssembly("IamApi.Migrations");
		});

		return new IAMDbContext(optionsBuilder.Options);
	}
}
