using DispatchR.Abstractions.Send;
using IamApi.Application.Users.Dtos.Response;
using IamApi.Domain.Entities;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.Users.Queries.GetUserById;

internal class GetUserByIdQueryHandler(
	IUnitOfWork unitOfWork,
	ILogger<GetUserByIdQueryHandler> logger) : IRequestHandler<GetUserByIdQuery, Task<GetUserByIdResponseDto>>
{
	public async Task<GetUserByIdResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Getting User with ID: {UserId} for Organization: {OrganizationId}", 
			request.userId, request.OrganizationId);

		var user = await unitOfWork.UsersRepository.GetUserByIdAsync(request.userId);

		if (user == null)
			throw new KeyNotFoundException($"User with ID {request.UserId} was not found");

		var userDto = user.Adapt<GetUserByIdResponseDto>();

		logger.LogDebug("User retrieved successfully: {UserId}", user.Id);

		return userDto;
	}
}
