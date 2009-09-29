using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Utility.Types {
	public class SvefakturaConversionSettings {
		public string InvoiceCurrencyCode;
		public string Postgiro { get; set; }
		public string PostgiroBankIdentificationCode { get; set; }
		public string BankGiro { get; set; }
		public string BankGiroBankIdentificationCode { get; set; }
		public decimal VATAmount { get; set; }
		public string SellingOrganizationNumber { get; set; }
		public string SellingOrganizationName { get; set; }
		public string SellingOrganizationAddress { get; set; }
		public string SellingOrganizationPostalCode { get; set; }
		public string SellingOrganizationCity { get; set; }
		public CountryIdentificationCodeContentType? SellingOrganizationCountryCode { get; set; }
		//public int NumberOfDecimalsUsedAtRounding { get; set; }
	}
}