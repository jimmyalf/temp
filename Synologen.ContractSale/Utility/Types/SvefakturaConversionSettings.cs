using System;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Invoicing.Types{
	public class SvefakturaConversionSettings 
	{
		public string VATFreeReasonMessage { get; set; }
		public SFTIContactType Contact { get; set; }
		//public string SellingOrganizationContactName { get; set; }
		//public string SellingOrganizationTelephone { get; set; }
		//public string SellingOrganizationFax { get; set; }
		//public string SellingOrganizationContactEmail { get; set; }
		public string ExemptionReason { get; set; }
		public string TaxAccountingCode { get; set; }
		public decimal? InvoiceExpieryPenaltySurchargePercent { get; set; }
		public CurrencyCodeContentType? InvoiceCurrencyCode { get; set; }
		public string InvoiceTypeCode { get; set; }
		public DateTime InvoiceIssueDate { get; set; }
		public string Postgiro { get; set; }
		public string PostgiroBankIdentificationCode { get; set; }
		public string BankGiro { get; set; }
		public string BankgiroBankIdentificationCode { get; set; }
		public decimal VATAmount { get; set; }
		public string SellingOrganizationNumber { get; set; }
		public string SellingOrganizationName { get; set; }
		//public string SellingOrganizationStreetName { get; set; }
		//public string SellingOrganizationPostBox { get; set; }
		//public string SellingOrganizationPostalCode { get; set; }
		//public string SellingOrganizationCity { get; set; }
		//public string SellingOrganizationRegistrationCity { get; set; }
		//public SFTICountryType SellingOrganizationCountry { get; set; }
		//public string SellingOrganizationCountryName { get; set; }
		public string InvoicePaymentTermsTextFormat { get; set; }
		public SFTIAddressType Adress { get; set; }
		public SFTIAddressType RegistrationAdress { get; set; }

		//public void SetAddress(string streetName, string postBox, string postalCode, string cityName, SFTICountryType country)
		//{
		//    if(Adress == null) Adress = new SFTIAddressType();
		//    if(!String.IsNullOrEmpty(streetName))
		//    {
		//        Adress.StreetName = new StreetNameType {Value = streetName};
		//    }
		//    if(!String.IsNullOrEmpty(postBox))
		//    {
		//        Adress.Postbox = new PostboxType {Value = postBox};
		//    }
		//    if(!String.IsNullOrEmpty(postalCode))
		//    {
		//        Adress.PostalZone = new ZoneType {Value = postalCode};
		//    }
		//    if(!String.IsNullOrEmpty(cityName))
		//    {
		//        Adress.CityName = new CityNameType {Value = cityName};
		//    }
		//    if(country != null)
		//    {
		//        Adress.Country = country;
		//    }
		//}
	}

}