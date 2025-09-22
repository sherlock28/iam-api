using DispatchR;
using IamApi.Application.Organizations.Commands.CreateOrganization;
using IamApi.Application.Organizations.Dtos.Response;
using IamApi.Application.Organizations.Queries.GetOrganizationById;
using Microsoft.AspNetCore.Mvc;

namespace IamApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController(IMediator dispatchR) : ControllerBase
{
	[HttpPost(Name = "CreateOrganization")]
	public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand request, CancellationToken cancellationToken)
	{
		var organizationId = await dispatchR.Send(request, cancellationToken);

		return CreatedAtAction(nameof(GetOrganizationById), new { organizationId }, null);
	}

	[HttpGet("{organizationId}", Name = "GetOrganizationById")]
	public async Task<ActionResult<GetOrganizationByIdResponseDto>> GetOrganizationById([FromRoute] Guid organizationId, CancellationToken cancellationToken)
	{
		var org = await dispatchR.Send(new GetOrganizationByIdQuery(organizationId), cancellationToken);

		return Ok(org);
	}
}
