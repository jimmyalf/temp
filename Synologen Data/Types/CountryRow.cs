using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Data.Types{
	public class CountryRow : ICountryRow{
		public int Id { get; set; }
		public string Name { get; set; }
		public CountryIdentificationCodeContentType? OrganizationCountryCode { get; set; }
	}
}