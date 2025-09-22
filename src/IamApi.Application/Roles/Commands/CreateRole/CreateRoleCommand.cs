using DispatchR.Abstractions.Send;

namespace IamApi.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<CreateRoleCommand, Task<Guid>>
{
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
}
