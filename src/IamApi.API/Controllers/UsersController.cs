using DispatchR;
using IamApi.Application.Users.Commands.CreateUser;
using IamApi.Application.Users.Dtos.Response;
using IamApi.Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

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

		return CreatedAtAction(nameof(GetUserById), new { organizationId, userId }, null);
	}

	[HttpGet("{userId}", Name = "GetUserById")]
	public async Task<ActionResult<GetUserByIdResponseDto>> GetUserById([FromRoute] Guid organizationId, [FromRoute] Guid userId, CancellationToken cancellationToken)
	{
		var user = await dispatchR.Send(new GetUserByIdQuery(organizationId, userId), cancellationToken);

		return Ok(user);
	}

	[HttpGet(Name = "GetUsersByFilters")]
	public IActionResult GetUsersByFilters([FromRoute] Guid organizationId)
	{
		return Ok();
	}
}
