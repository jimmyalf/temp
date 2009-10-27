using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Business.Interfaces{
	public interface ICountryRow{
		int Id { get; set; }
		string Name { get; set; }
		CountryIdentificationCodeContentType? OrganizationCountryCode { get; set; }
	}
}