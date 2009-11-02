
namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface ICountry{
		int Id { get; set; }
		string Name { get; set; }
		int? OrganizationCountryCodeId { get; set; }
	}
}