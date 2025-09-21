using DispatchR.Abstractions.Send;

namespace IamApi.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreateUserCommand, Task<Guid>>
{
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }
	public string Email { get; set; } = default!;
	public string Username { get; set; } = default!;
	public string Password { get; set; } = default!;
}
