namespace IamApi.Application.Organizations.Dtos.Response;

public class GetOrganizationByIdResponseDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime CreatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid? LastModifiedBy { get; }
	public DateTime? LastModifiedByAt { get; }
}
