namespace IamApi.Application.AppServices.Dtos.Response;

public class GetAppServiceByIdResponseDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public Guid OrganizationId { get; set; }
	public DateTime? CreatedAt { get; set; }
	public Guid? CreatedBy { get; set; }
	public DateTime? LastModifiedByAt { get; set; }
	public Guid? LastModifiedBy { get; set; }
}
