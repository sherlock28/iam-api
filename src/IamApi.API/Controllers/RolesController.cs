using DispatchR;
using IamApi.Application.Roles.Commands.CreateRole;
using IamApi.Application.Roles.Dtos.Response;
using IamApi.Application.Roles.Queries.GetRoleById;
using Microsoft.AspNetCore.Mvc;

namespace IamApi.API.Controllers;

[ApiController]
[Route("api/organizations/{organizationId}/[controller]")]
public class RolesController(IMediator dispatchR) : ControllerBase
{
	[HttpPost(Name = "CreateRole")]
	public async Task<IActionResult> CreateRole([FromRoute] Guid organizationId, [FromBody] CreateRoleCommand request, CancellationToken cancellationToken)
	{
		request.OrganizationId = organizationId;

		var roleId = await dispatchR.Send(request, cancellationToken);

		return CreatedAtAction(nameof(GetRoleById), new { organizationId, roleId }, null);
	}

	[HttpGet("{roleId}", Name = "GetRoleById")]
	public async Task<ActionResult<GetRoleByIdResponseDto>> GetRoleById([FromRoute] Guid organizationId, [FromRoute] Guid roleId, CancellationToken cancellationToken)
	{
		var role = await dispatchR.Send(new GetRoleByIdQuery(organizationId, roleId), cancellationToken);

		return Ok(role);
	}
}
