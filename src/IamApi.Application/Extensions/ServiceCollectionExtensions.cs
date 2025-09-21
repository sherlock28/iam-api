using Mapster;
using MapsterMapper;
using DispatchR.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using IamApi.Domain.Interfaces;
using IamApi.Application.Services;

namespace IamApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddApplication(this IServiceCollection services)
	{
		var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

		services.AddDispatchR(cfg => cfg.Assemblies.Add(applicationAssembly));

		// Mapster
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(applicationAssembly);
		services.AddSingleton(config);
		services.AddScoped<IMapper, Mapper>();

		// FluentValidation
		services.AddFluentValidationAutoValidation();

		// Custom Services
		services.AddScoped<ITenantProvider, TenantProvider>();
		services.AddHttpContextAccessor();
	}
}
