using IamApi.Domain.Entities;
using IamApi.Domain.Intefaces;

namespace IamApi.Domain.Entities;

public class Role : BaseEntity, IMultiTenant
{
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }

	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public List<RolePermission> RolePermissions { get; set; } = [];
}
