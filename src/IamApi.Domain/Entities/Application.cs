using IamApi.Domain.Entities;
using IamApi.Domain.Intefaces;

namespace IamApi.Domain.Entities;

public class Application : BaseEntity, ISoftDelete, IMultiTenant
{
	public string Name { get; set; } = default!;

	public Guid OrganizationId { get; set; }
	public Organization Organization { get; set; } = default!;

	public bool IsActive { get; set; } = true;
	public bool IsDeleted { get; set; } = false;

	// --- API Key (simple) ---
	public string? ApiKeyHash { get; set; }

	// --- OAuth2 Client Credentials ---
	public string? ClientId { get; set; }
	public string? ClientSecretHash { get; set; }

	public List<ApplicationRole> ApplicationRoles { get; set; } = [];
}
