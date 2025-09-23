using DispatchR.Abstractions.Send;
using IamApi.Domain.Entities;
using IamApi.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace IamApi.Application.AppServices.Commands.CreateAppService;

internal class CreateAppServiceCommandHandler(
	IUnitOfWork unitOfWork,
	ILogger<CreateAppServiceCommandHandler> logger) : IRequestHandler<CreateAppServiceCommand, Task<Guid>>
{
	public async Task<Guid> Handle(CreateAppServiceCommand request, CancellationToken cancellationToken)
	{
		logger.LogDebug("Creating AppService with data: {@AppService}", request);

		var service = request.Adapt<Service>();

		await unitOfWork.BeginTransactionAsync(cancellationToken);

		await unitOfWork.AppServiceRepository.AddAsync(service, cancellationToken);

		await unitOfWork.CommitTransactionAsync(cancellationToken);

		logger.LogInformation("AppService created successfully with ID: {AppServiceId}", service.Id);

		return service.Id;
	}
}
