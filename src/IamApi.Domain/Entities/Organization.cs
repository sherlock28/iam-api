using IamApi.Domain.Entities;

namespace Iam_api.Domain.Entities;

public class Organization : BaseEntity
{
	public string Name { get; set; } = default!;
	public bool IsActive { get; set; } = true;
}
