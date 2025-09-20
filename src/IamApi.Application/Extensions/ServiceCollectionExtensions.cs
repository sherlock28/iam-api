using Mapster;
using MapsterMapper;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Iam_api.Domain.Intefaces;
using Iam_api.Application.Services;

namespace IamApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddApplication(this IServiceCollection services)
	{
		var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

		// Mapster
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(applicationAssembly);
		services.AddSingleton(config);
		services.AddScoped<IMapper, Mapper>();

		// FluentValidation
		services.AddFluentValidationAutoValidation();

		// Custom Services
		services.AddScoped<ITenantProvider, TenantProvider>();
	}
}
