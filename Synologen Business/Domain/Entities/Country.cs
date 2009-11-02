using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class Country : ICountry{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? OrganizationCountryCodeId { get; set; }
	}
}