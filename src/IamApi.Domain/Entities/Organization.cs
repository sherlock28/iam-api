using IamApi.Domain.Entities;

namespace IamApi.Domain.Entities;

public class Organization : BaseEntity
{
	public string Name { get; set; } = default!;
	public bool IsActive { get; set; } = true;
}
