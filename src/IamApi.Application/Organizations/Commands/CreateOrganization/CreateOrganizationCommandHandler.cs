using DispatchR.Abstractions.Send;
using IamApi.Domain.Entities;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler(
	IUnitOfWork unitOfWork,
	ILogger<CreateOrganizationCommandHandler> logger) : IRequestHandler<CreateOrganizationCommand, Task<Guid>>
{
	public async Task<Guid> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Creating Organization with data: {@Organization}", request);

		var org = request.Adapt<Organization>();

		await unitOfWork.BeginTransactionAsync(cancellationToken);

		await unitOfWork.OrganizationRepository.AddAsync(org, cancellationToken);

		await unitOfWork.CommitTransactionAsync(cancellationToken);

		logger.LogInformation("Organization created successfully with ID: {OrgId}", org.Id);

		return org.Id;
	}
}
