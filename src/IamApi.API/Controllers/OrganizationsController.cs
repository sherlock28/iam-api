using DispatchR;
using IamApi.Application.Organizations.Commands.CreateOrganization;
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
		CancellationTokenSource cts = new();

		var orgId = await dispatchR.Send(request, cts.Token);

		return CreatedAtAction(nameof(GetOrganizationById), new { orgId }, null);
	}

	[HttpGet("{orgId}", Name = "GetOrganizationById")]
	public async Task<IActionResult> GetOrganizationById([FromRoute] Guid orgId)
	{
		CancellationTokenSource cts = new();

		var org = await dispatchR.Send(new GetOrganizationByIdQuery(orgId), cts.Token);

		return Ok(org);
	}
}
