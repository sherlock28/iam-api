namespace IamApi.API.Middlewares;

public class TenantMiddleware
{
	private readonly RequestDelegate _next;

	public TenantMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		// Solo validar rutas que requieren tenant
		if (context.Request.Path.StartsWithSegments("/api/organizations"))
		{
			if (!context.Request.RouteValues.ContainsKey("organizationId"))
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync("Organization ID is required");
				return;
			}
		}

		await _next(context);
	}
}
