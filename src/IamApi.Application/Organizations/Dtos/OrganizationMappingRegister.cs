using IamApi.Application.Organizations.Commands.CreateOrganization;
using IamApi.Application.Organizations.Dtos.Response;
using IamApi.Domain.Entities;
using Mapster;

namespace IamApi.Application.Organizations.Dtos;

public class OrganizationMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateOrganizationCommand, Organization>()
			.Map(dest => dest.Id, src => Guid.CreateVersion7());

		config.NewConfig<Organization, GetOrganizationByIdResponseDto>();
	}
}
