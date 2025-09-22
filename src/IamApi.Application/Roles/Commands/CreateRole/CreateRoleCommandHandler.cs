using DispatchR.Abstractions.Send;
using IamApi.Domain.Entities;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.Roles.Commands.CreateRole;

internal class CreateRoleCommandHandler(
	IUnitOfWork unitOfWork,
	ILogger<CreateRoleCommandHandler> logger) : IRequestHandler<CreateRoleCommand, Task<Guid>>
{
	public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Creating Role with data: {@Role}", request);

		var role = request.Adapt<Role>();

		await unitOfWork.BeginTransactionAsync(cancellationToken);

		await unitOfWork.RoleRepository.AddAsync(role, cancellationToken);

		await unitOfWork.CommitTransactionAsync(cancellationToken);

		logger.LogInformation("Role created successfully with ID: {roleId}", role.Id);

		return role.Id;
	}
}
