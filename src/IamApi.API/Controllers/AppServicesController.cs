using DispatchR;
using IamApi.Application.AppServices.Commands.CreateAppService;
using IamApi.Application.AppServices.Dtos.Response;
using IamApi.Application.AppServices.Queries.GetAppServiceById;
using Microsoft.AspNetCore.Mvc;

namespace IamApi.API.Controllers;

[ApiController]
[Route("api/organizations/{organizationId}/[controller]")]
public class AppServicesController(IMediator dispatchR) : ControllerBase
{
	[HttpPost(Name = "CreateAppService")]
	public async Task<IActionResult> CreateAppService([FromRoute] Guid organizationId, [FromBody] CreateAppServiceCommand request, CancellationToken cancellationToken)
	{
		var appServiceId = await dispatchR.Send(request, cancellationToken);

		return CreatedAtAction(nameof(GetAppServiceById), new { organizationId, appServiceId }, null);
	}

	[HttpGet("{appServiceId}", Name = "GetAppServiceById")]
	public async Task<ActionResult<GetAppServiceByIdResponseDto>> GetAppServiceById([FromRoute] Guid organizationId, [FromRoute] Guid appServiceId, CancellationToken cancellationToken)
	{
		var service = await dispatchR.Send(new GetAppServiceByIdQuery(organizationId, appServiceId), cancellationToken);

		return Ok(service);
	}
}
