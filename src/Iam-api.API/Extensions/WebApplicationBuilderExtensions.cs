using Serilog;

namespace Iam_api.API.Extensions;

public static class WebApplicationBuilderExtensions
{
	public static void AddPresentation(this WebApplicationBuilder builder)
	{
		builder.Services.AddProblemDetails();

		builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
	}
}
