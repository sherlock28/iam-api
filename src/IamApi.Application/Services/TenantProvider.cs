using IamApi.Domain.Intefaces;

namespace IamApi.Application.Services;

public class TenantProvider : ITenantProvider
{
	public Guid GetTenantId()
	{
		return Guid.CreateVersion7();
	}
}
