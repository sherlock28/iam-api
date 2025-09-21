using IamApi.Domain.Interfaces;

namespace IamApi.Domain.Entities;

public class Permission : BaseEntity, IMultiTenant
{
	public string Code { get; set; } = default!; // e.g. users.create, users.delete
	public string? Description { get; set; }

	public Guid OrganizationId { get; set; }
	public Organization Organization { get; set; } = default!;
}
