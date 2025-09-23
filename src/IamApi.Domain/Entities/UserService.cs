namespace IamApi.Domain.Entities;

public class UserService : BaseEntity
{
	public Guid UserId { get; set; }
	public User User { get; set; } = default!;
	public Guid ServiceId { get; set; }
	public Service Service { get; set; } = default!;
}
