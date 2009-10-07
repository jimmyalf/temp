using System;
using System.Configuration;
using System.Text;

namespace Spinit.Wpc.Synologen.ServiceLibrary.ConfigurationSettings {
	public class WebService : BaseConfiguration {
		public static readonly Encoding DefaultEncoding = Encoding.UTF8;

		public static int? InvoicingMethodIdFilter {
			get { return GetSafeValue("InvoicingMethodIdFilter", (int?)null); }
		}
		public static int NewSaleStatusId {
			get { return GetSafeValue("NewSaleStatusId", 1); }
		}

		public static int SaleStatusIdAfterSPCSImport {
			get { return GetSafeValue("SaleStatusIdAfterSPCSImport", 2); }
		}

		public static int SaleStatusIdAfterInvoicing {
			get { return GetSafeValue("SaleStatusIdAfterInvoicing", 5); }
		}

		public static int InvoicePayedToSynologenStatusId {
			get { return GetSafeValue("InvoicePayedToSynologenStatusId", 6); }
		}

		public static int InvoiceCancelledStatusId {
			get { return GetSafeValue("InvoiceCancelledStatusId", 7); }
		}

		public static string AdminEmail {
			get { return GetSafeValue("AdminEmail", String.Empty); }
		}

		public static string EmailSender {
			get { return GetSafeValue("SenderEmail", String.Empty); }
		}

		public static string SMTPServer {
			get { return GetSafeValue("SMTPServer", String.Empty); }
		}

		public static string BankGiro {
			get { return GetSafeValue("BankGiro", String.Empty); }
		}
		public static string Postgiro {
			get { return GetSafeValue("Postgiro", String.Empty); }
		}
		//public static int InvoiceExpieryNumberOfDaysOffset {
		//    get { return GetSafeValue("InvoiceExpieryNumberOfDaysOffset", 30); }
		//}
		//public static string EDIRecipientId {
		//    get { return GetSafeValue("EDIRecipientId", String.Empty); }
		//}
		public static string EDISenderId {
			get { return GetSafeValue("EDISenderId", String.Empty); }
		}
		public static float VATAmount {
			get { return GetSafeValue("VATAmount", 0.25F); }
		}
		public static string InvoiceCurrencyCode {
			get { return GetSafeValue("InvoiceCurrencyCode", String.Empty); }
		}

		public static bool SaveEDIFileCopy {
			get { return GetSafeValue("SaveEDIFileCopy", true); }
		}
		public static string EDIFilesFolderPath {
			get { return GetSafeValue("EDIFilesFolderPath", String.Empty); }
		}

		public static string ConnectionString {
			get { return ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString; }
		}

		public static bool SendAdminEmailOnError {
			get { return GetSafeValue("SendAdminEmailOnError", true); }
		}

		public static bool LogOther {
			get { return GetSafeValue("LogOther", true); }
		}

		public static bool LogInformation {
			get { return GetSafeValue("LogInformation", true); }
		}

		public static string FTPServerUrl {
			get { return GetSafeValue("FTPServerUrl", String.Empty); }
		}

		public static string FTPUserName {
			get { return GetSafeValue("FTPUserName", String.Empty); }
		}

		public static string FTPPassword {
			get { return GetSafeValue("FTPPassword", String.Empty); }
		}

		public static bool UsePassiveFTP {
			get { return GetSafeValue("UsePassiveFTP", true); }
		}

		public static Encoding FTPCustomEncodingCodePage {
			get {
				try{
					var encodingCodePage =  GetSafeValue("FTPCustomEncodingCodePage", String.Empty);
					return String.IsNullOrEmpty(encodingCodePage) ? DefaultEncoding : Encoding.GetEncoding(encodingCodePage);
				}
				catch { return DefaultEncoding; }
			}
		}

		public static bool FTPUseBinaryTransfer {
			get { return GetSafeValue("FTPUseBinaryTransfer", true); }
		}
	}
}