using System;
using System.Configuration;
using System.Text;
using Spinit.Wpc.Synologen.Business.Utility.Configuration;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.ConfigurationSettings 
{
    public class WebServiceConfiguration : BaseConfiguration, IOrderProcessConfiguration, IFileServiceConfiguration, IFtpServiceConfiguration, IMailServiceConfiguration
	{
		public readonly Encoding DefaultEncoding = Encoding.UTF8;

		public string SellingOrganizationCountryName{
			get { return GetSafeValue("SellingOrganizationCountryName", "Sverige"); }
		}

		public string PostnetRecipient {
			get { return GetSafeValue("PostnetRecipient", String.Empty); }
		}
		public string PostnetMessageType {
			get { return GetSafeValue("PostnetMessageType", String.Empty); }
		}

		public string PostnetSender {
			get { return GetSafeValue("PostnetSender", String.Empty); }
		}

		public int? InvoicingMethodIdFilter {
			get { return GetSafeValue("InvoicingMethodIdFilter", (int?)null); }
		}
		public int NewSaleStatusId {
			get { return GetSafeValue("NewSaleStatusId", 1); }
		}

		public int SaleStatusIdAfterSPCSImport {
			get { return GetSafeValue("SaleStatusIdAfterSPCSImport", 2); }
		}

		public int SaleStatusIdAfterInvoicing {
			get { return GetSafeValue("SaleStatusIdAfterInvoicing", 5); }
		}

		public int InvoicePayedToSynologenStatusId {
			get { return GetSafeValue("InvoicePayedToSynologenStatusId", 6); }
		}

		public int InvoiceCancelledStatusId {
			get { return GetSafeValue("InvoiceCancelledStatusId", 7); }
		}

		public string AdminEmail {
			get { return GetSafeValue("AdminEmail", String.Empty); }
		}

		public string ErrorEmailSenderAddress {
			get { return GetSafeValue("SenderEmail", String.Empty); }
		}
		public string StatusEmailSenderAddress {
			get { return GetSafeValue("StatusEmailSenderAddress", String.Empty); }
		}

		public string SMTPServer {
			get { return GetSafeValue("SMTPServer", String.Empty); }
		}

		public string BankGiro {
			get { return GetSafeValue("BankGiro", String.Empty); }
		}
		public string Postgiro {
			get { return GetSafeValue("Postgiro", String.Empty); }
		}

        public EdiAddress EDISenderId
        {
            get
            {
                var address = GetSafeValue("EDISenderId", String.Empty);
                var quantifier = GetSafeValue("EDISenderQuantifier", String.Empty);
                return new EdiAddress(address, quantifier);
            }
        }
		public float VATAmount {
			get { return GetSafeValue("VATAmount", 0.25F); }
		}
		public string InvoiceCurrencyCode {
			get { return GetSafeValue("InvoiceCurrencyCode", String.Empty); }
		}
		public bool SaveEDIFileCopy {
			get { return GetSafeValue("SaveEDIFileCopy", true); }
		}
		public bool SaveSvefakturaFileCopy {
			get { return GetSafeValue("SaveSvefakturaFileCopy", true); }
		}
		public string EDIFilesFolderPath {
			get { return GetSafeValue("EDIFilesFolderPath", String.Empty); }
		}

		public string ConnectionString {
			get { return ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString; }
		}

		public bool SendAdminEmailOnError {
			get { return GetSafeValue("SendAdminEmailOnError", true); }
		}

		public bool LogOther {
			get { return GetSafeValue("LogOther", true); }
		}

		public bool LogInformation {
			get { return GetSafeValue("LogInformation", true); }
		}

		public string FTPServerUrl {
			get { return GetSafeValue("FTPServerUrl", String.Empty); }
		}

		public string FTPUserName {
			get { return GetSafeValue("FTPUserName", String.Empty); }
		}

		public string FTPPassword {
			get { return GetSafeValue("FTPPassword", String.Empty); }
		}

		public bool UsePassiveFTP {
			get { return GetSafeValue("UsePassiveFTP", true); }
		}

		public Encoding FTPCustomEncodingCodePage {
			get {
				try{
					var encodingCodePage =  GetSafeValue("FTPCustomEncodingCodePage", String.Empty);
					return String.IsNullOrEmpty(encodingCodePage) ? DefaultEncoding : Encoding.GetEncoding(encodingCodePage);
				}
				catch { return DefaultEncoding; }
			}
		}

		public bool FTPUseBinaryTransfer {
			get { return GetSafeValue("FTPUseBinaryTransfer", true); }
		}
        
		
		
		#region Svefaktura specific settings
		public string BankGiroCode{
			get { return GetSafeValue("BankGiroCode", String.Empty); }
		}

		public string PostGiroCode{
			get { return GetSafeValue("PostGiroCode", (string) null); }
		}

		public string ExemptionReason{
			get { return GetSafeValue("ExemptionReason", String.Empty); }
		}

		public int CurrencyCodeId{
			get { return GetSafeValue("CurrencyCodeId", -1); }
		}

		public decimal? InvoiceExpieryPenaltySurchargePercent{
			get { return GetSafeValue("InvoiceExpieryPenaltySurchargePercent", -1m); }
		}

		public string InvoicePaymentTermsTextFormat{
			get { return GetSafeValue("InvoicePaymentTermsTextFormat", String.Empty); }
		}

		public string SvefakturaInvoiceTypeCode{
			get { return GetSafeValue("SvefakturaInvoiceTypeCode", String.Empty); }
		}

		public string SellingOrganizationCity{
			get { return GetSafeValue("SellingOrganizationCity", String.Empty); }
		}

		public string SellingOrganizationContactEmail{
			get { return GetSafeValue("SellingOrganizationContactEmail", String.Empty); }
		}

		public string SellingOrganizationContactName{
			get { return GetSafeValue("SellingOrganizationContactName", String.Empty); }
		}

		public int SellingOrganizationCountryCode{
			get { return GetSafeValue("SellingOrganizationCountryCode", -1); }
		}

		public string SellingOrganizationFax{
			get { return GetSafeValue("SellingOrganizationFax", String.Empty); }
		}

		public string SellingOrganizationName{
			get { return GetSafeValue("SellingOrganizationName", String.Empty); }
		}

		public string SellingOrganizationNumber{
			get { return GetSafeValue("SellingOrganizationNumber", String.Empty); }
		}

		public string SellingOrganizationPostalCode{
			get { return GetSafeValue("SellingOrganizationPostalCode", String.Empty); }
		}

		public string SellingOrganizationPostBox{
			get { return GetSafeValue("SellingOrganizationPostBox", (string) null); }
		}

		public string SellingOrganizationStreetName{
			get { return GetSafeValue("SellingOrganizationStreetName", String.Empty); }
		}

		public string SellingOrganizationTelephone{
			get { return GetSafeValue("SellingOrganizationTelephone", String.Empty); }
		}

		public string TaxAccountingCode{
			get { return GetSafeValue("TaxAccountingCode", String.Empty); }
		}

		public string VATFreeReasonMessage{
			get { return GetSafeValue("VATFreeReasonMessage", String.Empty); }
		}

		public string SellingOrganizationRegistrationCity
		{
			get { return GetSafeValue("SellingOrganizationRegistrationCity", String.Empty); }
		}

		#endregion
	}
}