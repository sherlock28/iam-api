using DispatchR.Abstractions.Send;
using IamApi.Application.AppServices.Dtos.Response;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.AppServices.Queries.GetAppServiceById;

internal class GetAppServiceByIdQueryHandler(
	IUnitOfWork unitOfWork,
	ILogger<GetAppServiceByIdQueryHandler> logger) : IRequestHandler<GetAppServiceByIdQuery, Task<GetAppServiceByIdResponseDto>>
{
	public async Task<GetAppServiceByIdResponseDto> Handle(GetAppServiceByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Getting AppService with ID: {AppServiceId} for Organization: {OrganizationId}",
			request.AppServiceId, request.OrganizationId);

		var appService = await unitOfWork.AppServiceRepository.GetServiceByIdAsync(request.AppServiceId);

		if (appService == null)
			throw new KeyNotFoundException($"AppService with ID {request.AppServiceId} was not found");

		var userDto = appService.Adapt<GetAppServiceByIdResponseDto>();

		logger.LogDebug("AppService retrieved successfully: {AppServiceId}", appService.Id);

		return userDto;
	}
}
