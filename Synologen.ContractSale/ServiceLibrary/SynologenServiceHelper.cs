using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Business.Extensions;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Convert=Spinit.Wpc.Synologen.Invoicing.Convert;

namespace Spinit.Wpc.Synologen.ServiceLibrary{
	public partial class SynologenService {

		private const string FtpFileUploadNotAccepted = "ej accepterad";
		private const string FtpFileUploadContainsError = "felaktig";
		private const int NumberOfDecimalsUsedForRounding = 2;
		private const string EDIFileNameFormat = "Synologen-{0}-{1}-{2}.txt";
		private const string SvefakturaFileNameFormat = "Synologen-{0}-{1}.xml";
		private const string SvefakturaListFileNameFormat = "Synologen-{0}-{1} {2}.xml";
		private const string DateFormat = "yyyy-MM-dd";

		private void SendInvoice(IOrder order) {
			try{
				string ftpStatusMessage;
				switch ((InvoicingMethod) order.ContractCompany.InvoicingMethodId){
					case InvoicingMethod.EDI:
						ftpStatusMessage = SendEDIInvoice(order);
						break;
					case InvoicingMethod.Svefaktura:
						ftpStatusMessage = SendSvefakturaInvoice(order);
						break;
					default:
						throw new ArgumentOutOfRangeException("order","Orders comany invoicing method cannot be identified.");
				}
				var newStatusId = ConfigurationSettings.WebService.SaleStatusIdAfterInvoicing;
				provider.UpdateOrderStatus(newStatusId, order.Id, 0, 0, 0, 0, 0);

				var orderHistoryMessage = GetInvoiceSentHistoryMessage(order.InvoiceNumber, ftpStatusMessage);
				provider.AddOrderHistory(order.Id, orderHistoryMessage);
				
			}
			catch(Exception ex) {
				throw LogAndCreateException("SynologenService.SendInvoice failed [OrderId: "+order.Id+"]", ex);
			}
		}

		private string SendEDIInvoice(IOrder order){
			var invoice = GenerateEDIInvoice(order);
			var invoiceFileName = GenerateInvoiceFileName(invoice);
			if(ConfigurationSettings.WebService.SaveEDIFileCopy) {
				TrySaveContentToDisk(invoiceFileName, invoice.Parse().ToString());
			}
			var invoiceString = invoice.Parse().ToString();
			return UploadTextFileToFTP(invoiceFileName, invoiceString);
		}

		private string SendSvefakturaInvoice(IOrder order){
			var invoice = GenerateSvefakturaInvoice(order);
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			if (ruleViolations.Any()){
				throw new WebserviceException("The invoice could not be validated: " + SvefakturaValidator.FormatRuleViolations(ruleViolations));
			}
			var encoding = ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
			var postOfficeheader = GetPostOfficeheader();
			var invoiceStringContent = SvefakturaSerializer.Serialize(invoice, encoding, "\r\n", Formatting.Indented, postOfficeheader);
			var invoiceFileName = GenerateInvoiceFileName(invoice);
			if(ConfigurationSettings.WebService.SaveSvefakturaFileCopy) {
				TrySaveContentToDisk(invoiceFileName, invoiceStringContent);
			}
			return UploadTextFileToFTP(invoiceFileName, invoiceStringContent);
		}

		private string SendSvefakturaInvoices(IEnumerable<Order> orders){
			var invoices = new SFTIInvoiceList{Invoices = new List<SFTIInvoiceType>()};
			foreach (var order in orders){
				var invoice = GenerateSvefakturaInvoice(order);
				var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
				if (ruleViolations.Any()){
					throw new WebserviceException("The invoice could not be validated: " + SvefakturaValidator.FormatRuleViolations(ruleViolations));
				}
				invoices.Invoices.Add(invoice);
			}
			var encoding = ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
			var postOfficeheader = GetPostOfficeheader();
			var invoiceStringContent = SvefakturaSerializer.Serialize(invoices, encoding, "\r\n", Formatting.Indented, postOfficeheader);
			var invoiceFileName = GenerateInvoiceFileName(invoices);
			if(ConfigurationSettings.WebService.SaveSvefakturaFileCopy) {
				TrySaveContentToDisk(invoiceFileName, invoiceStringContent);
			}
			return UploadTextFileToFTP(invoiceFileName, invoiceStringContent);
		}

		private void SendStatusReportAfterBatchInvoice(IEnumerable<int> orderIds, IEnumerable<int> sentOrderIds, string reportToEmail){
			try{
				var orderIdsNotSent = orderIds.Except(sentOrderIds);
				if(orderIdsNotSent != null && orderIdsNotSent.Count()>0){
					var subject = ServiceResources.resx.ServiceResources.BatchInvoiceFailureEmailSubject.Replace("{Date-Time}",DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
					var body = ServiceResources.resx.ServiceResources.BatchInvoiceFailureEmailBody
						.Replace("{Sent-Invoices}", sentOrderIds.ToFormattedString(", "))
						.Replace("{Not-Sent-Invoices}", orderIdsNotSent.ToFormattedString(", "));
					SendEmail(ConfigurationSettings.WebService.ErrorEmailSenderAddress, ConfigurationSettings.WebService.AdminEmail, subject, body);
					SendEmail(ConfigurationSettings.WebService.ErrorEmailSenderAddress, reportToEmail, subject, body);
				}
				else{
					var subject = ServiceResources.resx.ServiceResources.BatchInvoiceSuccessEmailSubject.Replace("{Date-Time}",DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
					var body = ServiceResources.resx.ServiceResources.BatchInvoiceSuccessEmailBody
						.Replace("{Sent-Invoices}", sentOrderIds.ToFormattedString(", "));
					SendEmail(ConfigurationSettings.WebService.StatusEmailSenderAddress, ConfigurationSettings.WebService.AdminEmail, subject, body);
					SendEmail(ConfigurationSettings.WebService.StatusEmailSenderAddress, reportToEmail, subject, body);
				}
			}
			catch{
				var errorMessage = String.Format(
					"Encountered error while sending batch invoice status report\r\nOrders not sent: {0}"
					,orderIds.Except(sentOrderIds)
				);
				TryLogErrorAndSendEmail(errorMessage);
			}
		}

		private static string GetPostOfficeheader(){
			const string headerFormat = "<?POSTNET SND=\"{0}\" REC=\"{1}\" MSGTYPE=\"{2}\"?>";
			var sender = ConfigurationSettings.WebService.PostnetSender;
			var recipient = ConfigurationSettings.WebService.PostnetRecipient;
			var messageType = ConfigurationSettings.WebService.PostnetMessageType;
			return String.Format(headerFormat, sender, recipient, messageType);
		}

		private static string GetVismaNewOrderAddedHistoryMessage(long vismaOrderId, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {
			var message = ServiceResources.resx.ServiceResources.OrderAddedToVismaHistoryMessage;
			message = message.Replace("{0}", vismaOrderId.ToString());
			message = message.Replace("{1}", invoiceSumIncludingVAT.ToString());
			message = message.Replace("{2}", invoiceSumExcludingVAT.ToString());
			return message;
		}

		private static string GetVismaOrderStatusUpdateHistoryMessage(long vismaOrderId) {
			var message = ServiceResources.resx.ServiceResources.OrderStatusUpdatedHistoryMessage;
			message = message.Replace("{0}", vismaOrderId.ToString());
			return message;
		}

		private static string GetInvoiceSentHistoryMessage(long invoiceNumber, string ftpStatusMessage) {
			var message = ServiceResources.resx.ServiceResources.InvoiceSentHistoryMessage;
			message = message.Replace("{0}", invoiceNumber.ToString());
			message = message.Replace("{1}", ftpStatusMessage);
			return message;
		}

		private static EDIConversionSettings GetEDISetting() {
			return new EDIConversionSettings
			{
				BankGiro = ConfigurationSettings.WebService.BankGiro,
				Postgiro = ConfigurationSettings.WebService.Postgiro,
				SenderId = ConfigurationSettings.WebService.EDISenderId,
				VATAmount = ConfigurationSettings.WebService.VATAmount,
				InvoiceCurrencyCode = ConfigurationSettings.WebService.InvoiceCurrencyCode,
				NumberOfDecimalsUsedAtRounding = NumberOfDecimalsUsedForRounding,
			};
		}

		private static SvefakturaConversionSettings GetSvefakturaSettings()
		{
			return new SvefakturaConversionSettings
			{
				InvoiceIssueDate = DateTime.Now,
				BankGiro = ConfigurationSettings.WebService.BankGiro,
				Postgiro = ConfigurationSettings.WebService.Postgiro,
				VATAmount = (decimal) ConfigurationSettings.WebService.VATAmount,
				BankgiroBankIdentificationCode = ConfigurationSettings.WebService.BankGiroCode,
				PostgiroBankIdentificationCode = ConfigurationSettings.WebService.PostGiroCode,
				ExemptionReason = ConfigurationSettings.WebService.ExemptionReason,
				InvoiceCurrencyCode = (CurrencyCodeContentType) ConfigurationSettings.WebService.CurrencyCodeId,
				InvoiceExpieryPenaltySurchargePercent = ConfigurationSettings.WebService.InvoiceExpieryPenaltySurchargePercent,
				InvoicePaymentTermsTextFormat = ConfigurationSettings.WebService.InvoicePaymentTermsTextFormat,
				InvoiceTypeCode = ConfigurationSettings.WebService.SvefakturaInvoiceTypeCode,
				Adress = new SFTIAddressType
				{
                    CityName = new CityNameType{ Value = ConfigurationSettings.WebService.SellingOrganizationCity},
                    Country = GetSellingOrganizationCountry(),
                    PostalZone = new ZoneType{Value = ConfigurationSettings.WebService.SellingOrganizationPostalCode},
					Postbox = ConfigurationSettings.WebService.SellingOrganizationPostBox.TryGetValue<PostboxType>((postbox, value) => postbox.Value = value),
                    StreetName = new StreetNameType{ Value = ConfigurationSettings.WebService.SellingOrganizationStreetName}
				},
				RegistrationAdress = new SFTIAddressType
				{
					CityName = ConfigurationSettings.WebService.SellingOrganizationRegistrationCity.TryGetValue<CityNameType>((city,value) => city.Value = value),
					Country = GetSellingOrganizationCountry(),
				},
				Contact = new SFTIContactType
				{
					ElectronicMail = new MailType{Value = ConfigurationSettings.WebService.SellingOrganizationContactEmail},
                    Name = new NameType{Value = ConfigurationSettings.WebService.SellingOrganizationContactName},
                    Telefax = new TelefaxType{Value = ConfigurationSettings.WebService.SellingOrganizationFax},
                    Telephone = new TelephoneType{Value = ConfigurationSettings.WebService.SellingOrganizationTelephone}
				},
				SellingOrganizationName = ConfigurationSettings.WebService.SellingOrganizationName,
				SellingOrganizationNumber = ConfigurationSettings.WebService.SellingOrganizationNumber,
				TaxAccountingCode = ConfigurationSettings.WebService.TaxAccountingCode,
				VATFreeReasonMessage = ConfigurationSettings.WebService.VATFreeReasonMessage,
			};
		}

		private static SFTICountryType GetSellingOrganizationCountry(){
			return new SFTICountryType {
				IdentificationCode = new CountryIdentificationCodeType {
					Value = (CountryIdentificationCodeContentType) ConfigurationSettings.WebService.SellingOrganizationCountryCode,
					name = ConfigurationSettings.WebService.SellingOrganizationCountryName
				}
			};
		}

		private static string GenerateInvoiceFileName(Invoice invoice) {
			var date = invoice.InterchangeHeader.DateOfPreparation.ToString(DateFormat);
			var referenceNumber = invoice.InterchangeControlReference;
			var invoiceNumber = invoice.DocumentNumber;
			return String.Format(EDIFileNameFormat, date, invoiceNumber, referenceNumber);
		}
		private static string GenerateInvoiceFileName(SFTIInvoiceType invoice) {
			var date = invoice.IssueDate.Value.ToString(DateFormat);
			var invoiceNumber = invoice.ID.Value;
			return String.Format(SvefakturaFileNameFormat, date, invoiceNumber);
		}
		private static string GenerateInvoiceFileName(SFTIInvoiceList invoices) {
			var maxId = invoices.Invoices.Max(x => x.ID.Value);
			var minId = invoices.Invoices.Min(x => x.ID.Value);
			return String.Format(SvefakturaListFileNameFormat, minId, maxId, DateTime.Now.ToString(DateFormat));
		}

		private string UploadTextFileToFTP(string fileName, string fileContent) {
			try {
				var ftp = GetFtpClientObject();
				var usePassiveFtp = ConfigurationSettings.WebService.UsePassiveFTP;
				var encoding = ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
				var useBinaryTransfer = ConfigurationSettings.WebService.FTPUseBinaryTransfer;
				var response = ftp.UploadStringAsFile(fileName, fileContent, usePassiveFtp, encoding, useBinaryTransfer);
				var responseStatusDescription = response.StatusDescription;
				//response.Close();
				CheckFtpUploadStatusDescriptionForErrorMessages(responseStatusDescription);
				return responseStatusDescription;
			}
			catch (Exception ex){
				throw LogAndCreateException("SynologenService.UploadTextFileToFTP failed", ex);
			}
		}

		private void TrySaveContentToDisk(string fileName, string fileContent) {
			var encoding = ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
			TrySaveContentToDisk(fileName, fileContent, encoding);
		}
		private void TrySaveContentToDisk(string fileName, string fileContent, Encoding encoding) {
			try {
				var path = ConfigurationSettings.WebService.EDIFilesFolderPath;
				if(!Directory.Exists(path)) {Directory.CreateDirectory(path); }
				var filePath = Path.Combine(path, fileName);
				File.AppendAllText(filePath, fileContent, encoding);
			}
			catch(Exception ex) {
				TryLogErrorAndSendEmail("SynologenService.SaveContentToDisk failed: " +ex.Message);
			}
		}

		private static Ftp GetFtpClientObject() {
			var ftpUrl = ConfigurationSettings.WebService.FTPServerUrl;
			var ftpUserName = ConfigurationSettings.WebService.FTPUserName;
			var ftpPassword = ConfigurationSettings.WebService.FTPPassword;
			var credentials = new NetworkCredential { UserName = ftpUserName, Password = ftpPassword };
			return new Ftp(ftpUrl, credentials);
		}

		private static void CheckFtpUploadStatusDescriptionForErrorMessages(string statusDescription) {
			statusDescription = statusDescription.ToLower().Trim();
			if (!statusDescription.StartsWith(Ftp.FileTransferCompleteResponseCode)) {
				throw new WebserviceException("Ftp transmission failed: " + statusDescription);
			}
			if (statusDescription.Contains(FtpFileUploadNotAccepted)) {
				throw new WebserviceException("Ftp transmission reported EDI file was not accepted: " + statusDescription);
			}
			if (statusDescription.Contains(FtpFileUploadContainsError)) {
				throw new WebserviceException("Ftp transmission reported EDI file contains errors: " + statusDescription);
			}
		}

		#region To Invoice Conversion
		private static Invoice GenerateEDIInvoice(IOrder order) {
			var ediSettings = GetEDISetting();
			var invoice = Convert.ToEDIInvoice(ediSettings, order);
			return invoice;
		}

		private static SFTIInvoiceType GenerateSvefakturaInvoice(IOrder order) {
			var settings = GetSvefakturaSettings();
			var invoice = Convert.ToSvefakturaInvoice(settings, order);
			return invoice;
		}
		#endregion

		#region Error Handling
		private Exception LogAndCreateException(string message, Exception ex) {
			var exception = new WebserviceException(message, ex);
			LogMessage(LogType.Error, ex.ToString());
			if(ConfigurationSettings.WebService.SendAdminEmailOnError) {
				TrySendErrorEmail(exception.ToString());
			}
			return exception;
		}

		private static void TrySendErrorEmail(string message) {
			try {
				var smtpClient = new SmtpClient(ConfigurationSettings.WebService.SMTPServer);
				var mailMessage = new MailMessage();
				mailMessage.To.Add(ConfigurationSettings.WebService.AdminEmail);
				mailMessage.From = new MailAddress(ConfigurationSettings.WebService.ErrorEmailSenderAddress);
				mailMessage.Subject = ServiceResources.resx.ServiceResources.ErrorEmailSubject;
				mailMessage.Body = message;
				smtpClient.Send(mailMessage);
			}
			catch{return;}
		}

		private void TryLogErrorAndSendEmail(string message) {
			try {
				LogMessage(LogType.Error, message);
			}
			catch { return; }
		}
		#endregion
	}

	public static class HelperExtensions
	{
		public static TType TryGetValue<TType>(this string value, Action<TType,string> actionIfValueIsNotNull)
			where TType : class, new()
		{
			if(value == null) return null;
			var returnValue = new TType();
			actionIfValueIsNotNull(returnValue, value);
			return returnValue;
		}
	}
}