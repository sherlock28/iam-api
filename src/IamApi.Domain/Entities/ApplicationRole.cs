using IamApi.Domain.Entities;

namespace IamApi.Domain.Entities;

public class ApplicationRole : BaseEntity
{
	public Guid ApplicationId { get; set; }
	public Application Application { get; set; } = default!;

	public Guid RoleId { get; set; }
	public Role Role { get; set; } = default!;
}
