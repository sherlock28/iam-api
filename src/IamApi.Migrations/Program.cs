using IamApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IamApi.Migrations;

class Program
{
	static async Task Main(string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		// Configure logging
		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();

		// Configure DbContext
		var connectionString = builder.Configuration.GetConnectionString("IAMDB");
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new InvalidOperationException("Connection string 'IAMDB' not found.");
		}

		builder.Services.AddDbContext<IAMDbContext>(options =>
		{
			options.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly("IamApi.Migrations");
				sqlOptions.CommandTimeout(300); // 5 minute timeout for large migrations
			});
		});

		var app = builder.Build();
		var logger = app.Services.GetRequiredService<ILogger<Program>>();

		try
		{
			using var scope = app.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<IAMDbContext>();

			logger.LogInformation("Starting database migration...");
			logger.LogInformation("Connection string: {ConnectionString}",
				connectionString.Substring(0, Math.Min(50, connectionString.Length)) + "...");

			// Check if the database can connect
			await context.Database.CanConnectAsync();
			logger.LogInformation("Database connection successful");

			// Get pending migrations
			var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
			var pendingMigrationsList = pendingMigrations.ToList();

			if (pendingMigrationsList.Count == 0)
			{
				logger.LogInformation("No pending migrations found. Database is up to date.");
				return;
			}

			logger.LogInformation("Found {Count} pending migrations:", pendingMigrationsList.Count);
			foreach (var migration in pendingMigrationsList)
			{
				logger.LogInformation("  - {Migration}", migration);
			}

			// Apply migrations
			logger.LogInformation("Applying migrations...");
			await context.Database.MigrateAsync();

			logger.LogInformation("✅ Migrations applied successfully!");

			// Verify applied migrations
			var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
			logger.LogInformation("Total applied migrations: {Count}", appliedMigrations.Count());
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "❌ Error applying migrations: {Message}", ex.Message);
			Environment.Exit(1);
		}
	}
}
