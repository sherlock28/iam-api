using DispatchR.Abstractions.Send;

namespace IamApi.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommand(string name) : IRequest<CreateOrganizationCommand, Task<Guid>>
{
	public string Name { get; } = name;
}
