using IamApi.Domain.Entities;
using IamApi.Domain.Interfaces;
using Mapster;
using DispatchR.Abstractions.Send;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.Users.Commands.CreateUser;

internal class CreateUserCommandHandler(
	IUnitOfWork unitOfWork,
	ILogger<CreateUserCommandHandler> logger) : IRequestHandler<CreateUserCommand, Task<Guid>>
{
	public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Creating User with data: {@User}", request);

		var user = request.Adapt<User>();

		await unitOfWork.BeginTransactionAsync(cancellationToken);

		await unitOfWork.UsersRepository.AddAsync(user, cancellationToken);

		await unitOfWork.CommitTransactionAsync(cancellationToken);

		logger.LogInformation("User created successfully with ID: {UserId}", user.Id);

		return user.Id;
	}
}
