using DispatchR.Abstractions.Send;
using IamApi.Application.AppServices.Dtos.Response;

namespace IamApi.Application.AppServices.Queries.GetAppServiceById;

public class GetAppServiceByIdQuery(Guid organizationId, Guid appServiceId) : IRequest<GetAppServiceByIdQuery, Task<GetAppServiceByIdResponseDto>>
{
	public Guid OrganizationId { get; } = organizationId;
	public Guid AppServiceId { get; } = appServiceId;
}
