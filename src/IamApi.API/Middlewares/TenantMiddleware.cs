using Microsoft.AspNetCore.Mvc;

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
		// Only check organizationId if the path actually contains it
		if (context.Request.Path.StartsWithSegments("/api/organizations") && 
		    context.Request.RouteValues.ContainsKey("organizationId"))
		{
			var orgId = context.Request.RouteValues["organizationId"]?.ToString();
			if (string.IsNullOrEmpty(orgId))
			{
				var problemDetails = new ProblemDetails
				{
					Status = StatusCodes.Status500InternalServerError,
					Title = "Bad request",
					Detail = "Organization ID is required",
					Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
					Instance = context.Request.Path
				};

				context.Response.StatusCode = problemDetails.Status.Value;
				context.Response.ContentType = "application/problem+json";


				await context.Response.WriteAsJsonAsync(problemDetails);

				return;
			}
		}

		await _next(context);
	}
}
