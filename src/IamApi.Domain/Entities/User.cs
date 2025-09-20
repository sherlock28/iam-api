using Iam_api.Domain.Entities;
using Iam_api.Domain.Intefaces;

namespace IamApi.Domain.Entities;

public class User : BaseEntity, ISoftDelete, IMultiTenant
{
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }

	public string Email { get; set; } = default!;
	public string Username { get; set; } = default!;
	public string PasswordHash { get; set; } = default!;
	public string? Salt { get; set; }

	public bool IsActive { get; set; } = true;
	public bool IsDeleted { get; set; } = false;

	public List<UserRole> UserRoles { get; set; } = [];
	public List<UserService> UserServices { get; set; } = [];
}
