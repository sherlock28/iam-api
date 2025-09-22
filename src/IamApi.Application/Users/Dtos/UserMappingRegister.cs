using IamApi.Domain.Entities;
using IamApi.Application.Users.Dtos.Response;
using IamApi.Application.Users.Commands.CreateUser;
using Mapster;

namespace IamApi.Application.Users.Dtos;

public class UserMappingRegister : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateUserCommand, User>()
			.Map(dest => dest.Id, src => Guid.CreateVersion7())
			.Map(dest => dest.OrganizationId, src => src.OrganizationId)
			.Map(dest => dest.ServiceId, src => src.ServiceId)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.NormalizedEmail, src => src.Email.ToUpperInvariant())
			.Map(dest => dest.Username, src => src.Username)
			.Map(dest => dest.NormalizedUsername, src => src.Username.ToUpperInvariant())
			.Map(dest => dest.PasswordHash, src => src.Password) // TODO: Implement proper password hashing
			.Map(dest => dest.IsActive, src => true)
			.Map(dest => dest.IsDeleted, src => false)
			.Map(dest => dest.LockoutEnabled, src => false)
			.Map(dest => dest.AccessFailedCount, src => 0)
			.Map(dest => dest.LockoutEnd, src => DateTime.MinValue)
			.Map(dest => dest.UserRoles, src => new List<UserRole>())
			.Map(dest => dest.UserServices, src => new List<UserService>());

		config.NewConfig<User, GetUserByIdResponseDto>()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.OrganizationId, src => src.OrganizationId)
			.Map(dest => dest.ServiceId, src => src.ServiceId)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.Username, src => src.Username)
			.Map(dest => dest.IsActive, src => src.IsActive)
			.Map(dest => dest.CreatedBy, src => src.CreatedBy)
			.Map(dest => dest.CreatedAt, src => src.CreatedAt)
			.Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy)
			.Map(dest => dest.LastModifiedByAt, src => src.LastModifiedByAt);
	}
}
