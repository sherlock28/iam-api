namespace IamApi.Domain.Entities;

public class BaseEntity
{
	public Guid Id { get; set; } = Guid.CreateVersion7();
	public DateTime? CreatedAt { get; set; }
	public Guid? CreatedBy { get; set; }
	public DateTime? LastModifiedByAt { get; set; }
	public Guid? LastModifiedBy { get; set; }
}
