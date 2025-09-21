using IamApi.Domain.Interfaces;

namespace IamApi.Domain.Entities;

public class Service : BaseEntity, IMultiTenant
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public Guid OrganizationId { get; set; }
}
