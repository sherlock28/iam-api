using IamApi.Application.AppServices.Commands.CreateAppService;
using IamApi.Application.AppServices.Dtos.Response;
using IamApi.Domain.Entities;
using Mapster;

namespace IamApi.Application.AppServices.Dtos;

public class AppServiceMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateAppServiceCommand, Service>()
			.Map(dest => dest.Id, src => Guid.CreateVersion7());

		config.NewConfig<Service, GetAppServiceByIdResponseDto>();
	}
}
