namespace IamApi.Application.Users.Dtos.Response;

public class GetUserByIdResponseDto
{
	public Guid Id { get; set; }
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }
	public string Email { get; set; } = default!;
	public string Username { get; set; } = default!;
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid? LastModifiedBy { get; }
	public DateTime? LastModifiedByAt { get; }
}
