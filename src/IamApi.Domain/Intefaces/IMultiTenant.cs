namespace Iam_api.Domain.Intefaces;

public interface IMultiTenant
{
	Guid OrganizationId { get; set; }
}
