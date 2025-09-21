namespace IamApi.Domain.Entities;

public class Organization : BaseEntity
{
	public string Name { get; set; } = default!;
	public bool IsActive { get; set; } = true;

	public List<UsrApplication> OwnedUsrApplications { get; set; } = [];
	public List<User> OwnedUsers { get; set; } = [];
	public List<Role> OwnedRoles { get; set; } = [];
	public List<Permission> OwnedPermissions { get; set; } = [];
}
