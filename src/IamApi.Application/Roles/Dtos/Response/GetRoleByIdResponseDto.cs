namespace IamApi.Application.Roles.Dtos.Response;

public class GetRoleByIdResponseDto
{
	public Guid Id { get; set; }
	public Guid OrganizationId { get; set; }
	public Guid ServiceId { get; set; }
	public string Name { get; set; } = default!;
	public string NormalizedName { get; set; } = default!;
	public string Description { get; set; } = default!;
	public DateTime CreatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid? LastModifiedBy { get; }
	public DateTime? LastModifiedByAt { get; }
}
