using IamApi.Application.Roles.Commands.CreateRole;
using IamApi.Application.Roles.Dtos.Response;
using IamApi.Domain.Entities;
using Mapster;

namespace IamApi.Application.Roles.Dtos;

public class RoleMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateRoleCommand, Role>()
			.Map(dest => dest.Id, src => Guid.CreateVersion7())
			.Map(dest => dest.NormalizedName, src => src.Name.ToUpperInvariant());

		config.NewConfig<Organization, GetRoleByIdResponseDto>();
	}
}
