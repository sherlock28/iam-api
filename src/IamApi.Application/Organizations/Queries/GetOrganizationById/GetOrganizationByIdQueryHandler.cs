using DispatchR.Abstractions.Send;
using IamApi.Application.Organizations.Dtos.Response;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.Organizations.Queries.GetOrganizationById;

public class GetOrganizationByIdQueryHandler(
	IUnitOfWork unitOfWork,
	ILogger<GetOrganizationByIdQueryHandler> logger) : IRequestHandler<GetOrganizationByIdQuery, Task<GetOrganizationByIdResponseDto>>
{
	public async Task<GetOrganizationByIdResponseDto> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Getting Organization with ID: {OrganizationId}", request.organizationId);

		var org = await unitOfWork.OrganizationRepository.GetOrganizationByIdAsync(request.organizationId);

		if (org == null)
			throw new KeyNotFoundException($"Organization with ID {request.organizationId} was not found");

		var orgDto = org.Adapt<GetOrganizationByIdResponseDto>();

		logger.LogDebug("Organization retrieved successfully: {UserId}", orgDto.Id);

		return orgDto;
	}
}
