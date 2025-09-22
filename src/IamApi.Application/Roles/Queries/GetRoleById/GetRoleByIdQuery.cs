using DispatchR.Abstractions.Send;
using IamApi.Application.Roles.Dtos.Response;

namespace IamApi.Application.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(Guid organizationId, Guid roleId) : IRequest<GetRoleByIdQuery, Task<GetRoleByIdResponseDto?>>;
