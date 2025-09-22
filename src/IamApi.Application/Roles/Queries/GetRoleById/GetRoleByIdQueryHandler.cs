using DispatchR.Abstractions.Send;
using IamApi.Application.Roles.Dtos.Response;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.Roles.Queries.GetRoleById;

internal class GetRoleByIdQueryHandler(
	IUnitOfWork unitOfWork,
	ILogger<GetRoleByIdQueryHandler> logger) : IRequestHandler<GetRoleByIdQuery, Task<GetRoleByIdResponseDto?>>
{
	public async Task<GetRoleByIdResponseDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Getting Role with ID: {RoleId} for Organization: {OrganizationId}",
			request.roleId, request.organizationId);

		var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.roleId);

		if (role == null)
			throw new KeyNotFoundException($"Role with ID {request.roleId} was not found");

		var roleDto = role.Adapt<GetRoleByIdResponseDto>();

		logger.LogDebug("User retrieved successfully: {UserId}", role.Id);

		return roleDto;
	}
}
