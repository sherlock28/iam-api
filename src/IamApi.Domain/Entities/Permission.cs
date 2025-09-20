using IamApi.Domain.Entities;

namespace Iam_api.Domain.Entities;

public class Permission : BaseEntity
{
	public string Code { get; set; } = default!; // e.g. users.create, users.delete
	public string? Description { get; set; }
}
