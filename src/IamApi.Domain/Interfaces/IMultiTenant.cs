namespace IamApi.Domain.Interfaces;

public interface IMultiTenant
{
	Guid OrganizationId { get; set; }
}
