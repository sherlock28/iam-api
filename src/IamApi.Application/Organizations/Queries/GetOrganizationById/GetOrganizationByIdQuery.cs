using DispatchR.Abstractions.Send;
using IamApi.Application.Organizations.Dtos.Response;

namespace IamApi.Application.Organizations.Queries.GetOrganizationById;

public record GetOrganizationByIdQuery(Guid orgId) : IRequest<GetOrganizationByIdQuery, Task<GetOrganizationByIdResponseDto?>>;
