namespace IamApi.Domain.Entities;

public class User : BaseEntity
{
	public Guid Id { get; set; }
	public Guid OrganizationId { get; set; }
	public Guid? ServiceId { get; set; }


	public string Email { get; set; } = default!;
	public string Username { get; set; } = default!;
	public string PasswordHash { get; set; } = default!;
	public string? Salt { get; set; }


	public bool IsActive { get; set; } = true;
	public bool IsDeleted { get; set; } = false;
}
