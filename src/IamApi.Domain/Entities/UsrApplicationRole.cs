namespace IamApi.Domain.Entities;

public class UsrApplicationRole : BaseEntity
{
	public Guid UsrApplicationId { get; set; }
	public UsrApplication UsrApplication { get; set; } = default!;

	public Guid RoleId { get; set; }
	public Role Role { get; set; } = default!;
}
