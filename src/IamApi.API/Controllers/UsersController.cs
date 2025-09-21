using DispatchR;
using Microsoft.AspNetCore.Mvc;
using IamApi.Application.Users.Commands.CreateUser;
using IamApi.Application.Users.Queries.GetUserById;

namespace IamApi.API.Controllers;

[ApiController]
[Route("api/organizations/{organizationId}/[controller]")]
public class UsersController(IMediator dispatchR) : ControllerBase
{
	[HttpPost(Name = "CreateUser")]
	public async Task<IActionResult> CreateUser([FromRoute] Guid organizationId, [FromBody] CreateUserCommand request, CancellationToken cancellationToken)
	{
		request.OrganizationId = organizationId;

		var userId = await dispatchR.Send(request, cancellationToken);

		return CreatedAtAction(nameof(CreateUser), new { organizationId, userId }, null);
	}

	[HttpGet("{userId}", Name = "GetUserById")]
	public async Task<IActionResult> GetUserById([FromRoute] Guid organizationId, [FromRoute] Guid userId, CancellationToken cancellationToken)
	{
		var user = await dispatchR.Send(new GetUserByIdQuery(organizationId,userId), cancellationToken);

		return Ok(user);
	}

	[HttpGet(Name = "GetUsersByFilters")]
	public IActionResult GetUsersByFilters([FromRoute] Guid organizationId)
	{
		return Ok();
	}
}
