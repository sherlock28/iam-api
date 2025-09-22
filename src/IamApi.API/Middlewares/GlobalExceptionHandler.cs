using System;
using System.Threading;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IamApi.API.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		logger.LogError(exception, "An unhandled exception has occurred with message: {message}", exception.Message);

		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Title = "Internal Server Error",
			Detail = exception.Message,
			Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
			Instance = httpContext.Request.Path
		};

		httpContext.Response.StatusCode = problemDetails.Status.Value;
		httpContext.Response.ContentType = "application/problem+json";

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}
