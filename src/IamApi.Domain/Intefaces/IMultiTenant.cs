namespace IamApi.Domain.Intefaces;

public interface IMultiTenant
{
	Guid OrganizationId { get; set; }
}
