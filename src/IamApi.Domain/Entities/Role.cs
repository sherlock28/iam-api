using Iam_api.Domain.Intefaces;
using IamApi.Domain.Entities;

namespace Iam_api.Domain.Entities;

public class Role : BaseEntity, IMultiTenant
{
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }

	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public List<RolePermission> RolePermissions { get; set; } = [];
}
