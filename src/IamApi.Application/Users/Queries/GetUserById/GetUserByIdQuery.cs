using DispatchR.Abstractions.Send;
using IamApi.Application.Users.Dtos.Response;

namespace IamApi.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid organizationId, Guid userId) : IRequest<GetUserByIdQuery, Task<GetUserByIdResponseDto>>
{
	public Guid OrganizationId { get; } = organizationId;
	public Guid UserId { get; } = userId;
}
