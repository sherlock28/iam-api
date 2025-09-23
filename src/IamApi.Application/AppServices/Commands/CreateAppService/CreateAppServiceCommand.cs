using DispatchR.Abstractions.Send;

namespace IamApi.Application.AppServices.Commands.CreateAppService;

public class CreateAppServiceCommand : IRequest<CreateAppServiceCommand, Task<Guid>>
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public Guid OrganizationId { get; set; }
}
