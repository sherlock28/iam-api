namespace IamApi.Domain.Interfaces;

public interface ITenantProvider
{
	Guid? GetCurrentTenantId();
	bool HasTenant();
}
