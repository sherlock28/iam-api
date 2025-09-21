using IamApi.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IamApi.Application.Services;

public class TenantProvider : ITenantProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public TenantProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public Guid? GetCurrentTenantId()
	{
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext?.Request.RouteValues.TryGetValue("organizationId", out var tenantId) == true)
		{
			var tenantIdString = tenantId?.ToString();
			if (!string.IsNullOrEmpty(tenantIdString))
			{
				return Guid.Parse(tenantIdString);
			}
		}

		return null;
	}

	public bool HasTenant() => GetCurrentTenantId() == null;
}
