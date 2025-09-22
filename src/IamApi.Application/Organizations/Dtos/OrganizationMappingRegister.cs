using IamApi.Domain.Entities;
using IamApi.Application.Organizations.Queries.GetOrganizationById;
using IamApi.Application.Organizations.Commands.CreateOrganization;
using Mapster;

namespace IamApi.Application.Organizations.Dtos;

public class OrganizationMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateOrganizationCommand, Organization>()
			.Map(dest => dest.Id, src => Guid.CreateVersion7());

		config.NewConfig<Organization, GetOrganizationByIdQuery>();
	}
}
