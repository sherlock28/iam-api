using IamApi.Domain.Entities;

namespace IamApi.Domain.Entities;

public class Permission : BaseEntity
{
	public string Code { get; set; } = default!; // e.g. users.create, users.delete
	public string? Description { get; set; }
}
