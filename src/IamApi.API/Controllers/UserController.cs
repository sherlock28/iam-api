using Microsoft.AspNetCore.Mvc;

namespace IamApi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger) : ControllerBase
{
	[HttpGet(Name = "GetUsersByFilters")]
	public IActionResult GetUsersByFilters()
	{
		logger.LogInformation("Retrieving users with filters...");

		return Ok();
	}
}
