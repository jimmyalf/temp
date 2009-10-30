using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class Country : ICountry{
		public int Id { get; set; }
		public string Name { get; set; }
		public CountryIdentificationCodeContentType? OrganizationCountryCode { get; set; }
	}
}