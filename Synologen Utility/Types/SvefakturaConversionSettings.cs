using System;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Invoicing.Types{
	public class SvefakturaConversionSettings {
		public string VATFreeReasonMessage { get; set; }
		public string SellingOrganizationContactName { get; set; }
		public string SellingOrganizationTelephone { get; set; }
		public string SellingOrganizationFax { get; set; }
		public string SellingOrganizationContactEmail { get; set; }
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
		public string SellingOrganizationStreetName { get; set; }
		public string SellingOrganizationPostBox { get; set; }
		public string SellingOrganizationPostalCode { get; set; }
		public string SellingOrganizationCity { get; set; }
		public CountryIdentificationCodeContentType? SellingOrganizationCountryCode { get; set; }
		public string InvoicePaymentTermsTextFormat { get; set; }
	}
}