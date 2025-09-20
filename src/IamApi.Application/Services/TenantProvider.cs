using Iam_api.Domain.Intefaces;

namespace Iam_api.Application.Services;

public class TenantProvider : ITenantProvider
{
	public Guid GetTenantId()
	{
		return Guid.CreateVersion7();
	}
}
