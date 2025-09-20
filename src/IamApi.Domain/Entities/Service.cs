using IamApi.Domain.Entities;

namespace IamApi.Domain.Entities;

public class Service : BaseEntity
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
}
