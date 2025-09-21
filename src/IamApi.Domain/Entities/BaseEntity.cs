namespace IamApi.Domain.Entities;

public class BaseEntity
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public DateTime? CreatedAt { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime? LastModifiedByAt { get; set; }
	public string? LastModifiedBy { get; set; }
}
