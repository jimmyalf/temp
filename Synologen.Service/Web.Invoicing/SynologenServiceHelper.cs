using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Extensions;
using Synologen.Service.Web.Invoicing.OrderProcessing;

namespace Synologen.Service.Web.Invoicing
{
	public partial class SynologenService 
    {
		//private const string FtpFileUploadNotAccepted = "ej accepterad";
		//private const string FtpFileUploadContainsError = "felaktig";
		//private const int NumberOfDecimalsUsedForRounding = 2;
		//private const string EDIFileNameFormat = "Synologen-{0}-{1}-{2}.txt";
		//private const string SvefakturaFileNameFormat = "Synologen-{0}-{1}.xml";
		//private const string SvefakturaListFileNameFormat = "Synologen-{0}-{1} {2}.xml";
		//private const string DateFormat = "yyyy-MM-dd";

        //private string SendEDIInvoice(IOrder order)
        //{
        //    try
        //    {
        //        var invoice = GenerateEDIInvoice(order);
        //        var invoiceFileName = GenerateInvoiceFileName(invoice);
        //        if (WebService.SaveEDIFileCopy) 
        //        {
        //            TrySaveContentToDisk(invoiceFileName, invoice.Parse().ToString());
        //        }

        //        var invoiceString = invoice.Parse().ToString();
        //        return UploadTextFileToFTP(invoiceFileName, invoiceString);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw LogAndCreateException("SynologenService.SendInvoice failed [OrderId: " + order.Id + "]", ex);
        //    }
        //}

        //private string SendSvefakturaInvoice(IOrder order){
        //    var invoice = GenerateSvefakturaInvoice(order);
        //    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
        //    if (ruleViolations.Any()){
        //        throw new WebserviceException("The invoice could not be validated: " + SvefakturaValidator.FormatRuleViolations(ruleViolations));
        //    }
        //    var encoding = ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
        //    var postOfficeheader = GetPostOfficeheader();
        //    var invoiceStringContent = SvefakturaSerializer.Serialize(invoice, encoding, "\r\n", Formatting.Indented, postOfficeheader);
        //    var invoiceFileName = GenerateInvoiceFileName(invoice);
        //    if(ConfigurationSettings.WebService.SaveSvefakturaFileCopy) {
        //        TrySaveContentToDisk(invoiceFileName, invoiceStringContent);
        //    }
        //    return UploadTextFileToFTP(invoiceFileName, invoiceStringContent);
        //}

        //private string SendLetterInvoices(IEnumerable<Order> orders)
        //{
        //    var invoices = new SFTIInvoiceList{ Invoices = new List<SFTIInvoiceType>() };
        //    foreach (var order in orders)
        //    {
        //        var invoice = _eBrevSvefakturaBuilder.Build(order);

        //        var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
        //        if (ruleViolations.Any())
        //        {
        //            throw new WebserviceException("The invoice could not be validated: " + SvefakturaValidator.FormatRuleViolations(ruleViolations));
        //        }

        //        invoices.Invoices.Add(invoice);
        //    }

        //    var encoding = WebService.FTPCustomEncodingCodePage;
        //    var postOfficeheader = GetPostOfficeheader();
        //    var invoiceStringContent = SvefakturaSerializer.Serialize(invoices, encoding, "\r\n", Formatting.Indented, postOfficeheader);
        //    var invoiceFileName = GenerateInvoiceFileName(invoices);
        //    if (WebService.SaveSvefakturaFileCopy) 
        //    {
        //        TrySaveContentToDisk(invoiceFileName, invoiceStringContent);
        //    }

        //    return UploadTextFileToFTP(invoiceFileName, invoiceStringContent);
        //}

		private void SendStatusReportAfterBatchInvoice(OrderProcessResult result, string reportToEmail)
        {
			try
            {
                if (result.FailedOrders.Any())
                {
					var subject = ServiceResources.resx.ServiceResources.BatchInvoiceFailureEmailSubject.Replace("{Date-Time}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
					var body = ServiceResources.resx.ServiceResources.BatchInvoiceFailureEmailBody
						.Replace("{Sent-Invoices}", result.SentOrdersIds.ToFormattedString(", "))
						.Replace("{Not-Sent-Invoices}", result.FailedOrders.Select(x => x.OrderId).ToFormattedString(", "));
                    TrySendErrorEmail(result.GetErrorDetails("\r\n\r\n"));
					SendEmail(_config.ErrorEmailSenderAddress, _config.EmailAdminAddress, subject, body);
                    SendEmail(_config.ErrorEmailSenderAddress, reportToEmail, subject, body);
                }
				else
                {
					var subject = ServiceResources.resx.ServiceResources.BatchInvoiceSuccessEmailSubject.Replace("{Date-Time}",DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
					var body = ServiceResources.resx.ServiceResources.BatchInvoiceSuccessEmailBody
						.Replace("{Sent-Invoices}", result.SentOrdersIds.ToFormattedString(", "));
					SendEmail(_config.StatusEmailSenderAddress, _config.EmailAdminAddress, subject, body);
					SendEmail(_config.StatusEmailSenderAddress, reportToEmail, subject, body);
				}
			}
			catch
			{
			    var notSentOrders = result.FailedOrders.Select(x => x.OrderId);
				var errorMessage = string.Format("Encountered error while sending batch invoice status report\r\nOrders not sent: {0}", notSentOrders);
				TryLogErrorAndSendEmail(errorMessage);
			}
		}

        //private static string GetPostOfficeheader()
        //{
        //    const string HeaderFormat = "<?POSTNET SND=\"{0}\" REC=\"{1}\" MSGTYPE=\"{2}\"?>";
        //    var sender = WebService.PostnetSender;
        //    var recipient = WebService.PostnetRecipient;
        //    var messageType = WebService.PostnetMessageType;
        //    return string.Format(HeaderFormat, sender, recipient, messageType);
        //}

		private static string GetVismaNewOrderAddedHistoryMessage(long vismaOrderId, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) 
        {
			var message = ServiceResources.resx.ServiceResources.OrderAddedToVismaHistoryMessage;
			message = message.Replace("{0}", vismaOrderId.ToString());
			message = message.Replace("{1}", invoiceSumIncludingVAT.ToString());
			message = message.Replace("{2}", invoiceSumExcludingVAT.ToString());
			return message;
		}

		private static string GetVismaOrderStatusUpdateHistoryMessage(long vismaOrderId) 
        {
			var message = ServiceResources.resx.ServiceResources.OrderStatusUpdatedHistoryMessage;
			message = message.Replace("{0}", vismaOrderId.ToString());
			return message;
		}

        //private static string GetInvoiceSentHistoryMessage(long invoiceNumber, string ftpStatusMessage) 
        //{
        //    var message = ServiceResources.resx.ServiceResources.InvoiceSentHistoryMessage;
        //    message = message.Replace("{0}", invoiceNumber.ToString());
        //    message = message.Replace("{1}", ftpStatusMessage);
        //    return message;
        //}

        //private static EDIConversionSettings GetEDISetting()
        //{
        //    return new EDIConversionSettings
        //    {
        //        BankGiro = WebService.BankGiro,
        //        Postgiro = WebService.Postgiro,
        //        SenderEdiAddress = WebService.EDISenderId,
        //        VATAmount = WebService.VATAmount,
        //        InvoiceCurrencyCode = WebService.InvoiceCurrencyCode,
        //        NumberOfDecimalsUsedAtRounding = NumberOfDecimalsUsedForRounding,
        //    };
        //}

        //private static SvefakturaConversionSettings GetSvefakturaSettings()
        //{
        //    return new SvefakturaConversionSettings
        //    {
        //        InvoiceIssueDate = DateTime.Now,
        //        BankGiro = WebService.BankGiro,
        //        Postgiro = WebService.Postgiro,
        //        VATAmount = (decimal)WebService.VATAmount,
        //        BankgiroBankIdentificationCode = WebService.BankGiroCode,
        //        PostgiroBankIdentificationCode = WebService.PostGiroCode,
        //        ExemptionReason = WebService.ExemptionReason,
        //        InvoiceCurrencyCode = (CurrencyCodeContentType) WebService.CurrencyCodeId,
        //        InvoiceExpieryPenaltySurchargePercent = WebService.InvoiceExpieryPenaltySurchargePercent,
        //        InvoicePaymentTermsTextFormat = WebService.InvoicePaymentTermsTextFormat,
        //        InvoiceTypeCode = WebService.SvefakturaInvoiceTypeCode,
        //        Adress = new SFTIAddressType
        //        {
        //            CityName = new CityNameType{ Value = WebService.SellingOrganizationCity},
        //            Country = GetSellingOrganizationCountry(),
        //            PostalZone = new ZoneType{Value = WebService.SellingOrganizationPostalCode},
        //            Postbox = WebService.SellingOrganizationPostBox
        //                .TryGetValue<PostboxType>((postbox, value) => postbox.Value = value),
        //            StreetName = new StreetNameType{ Value = WebService.SellingOrganizationStreetName}
        //        },
        //        RegistrationAdress = new SFTIAddressType
        //        {
        //            CityName = WebService.SellingOrganizationRegistrationCity.TryGetValue<CityNameType>((city, value) => city.Value = value),
        //            Country = GetSellingOrganizationCountry(),
        //        },
        //        Contact = new SFTIContactType
        //        {
        //            ElectronicMail = new MailType { Value = WebService.SellingOrganizationContactEmail },
        //            Name = new NameType { Value = WebService.SellingOrganizationContactName },
        //            Telefax = new TelefaxType { Value = WebService.SellingOrganizationFax },
        //            Telephone = new TelephoneType { Value = WebService.SellingOrganizationTelephone }
        //        },
        //        SellingOrganizationName = WebService.SellingOrganizationName,
        //        SellingOrganizationNumber = WebService.SellingOrganizationNumber,
        //        TaxAccountingCode = WebService.TaxAccountingCode,
        //        VATFreeReasonMessage = WebService.VATFreeReasonMessage,
        //    };
        //}

        //private static SFTICountryType GetSellingOrganizationCountry()
        //{
        //    return new SFTICountryType 
        //    {
        //        IdentificationCode = new CountryIdentificationCodeType 
        //        {
        //            Value = (CountryIdentificationCodeContentType)WebService.SellingOrganizationCountryCode,
        //            name = WebService.SellingOrganizationCountryName
        //        }
        //    };
        //}

        //private static string GenerateInvoiceFileName(Invoice invoice) 
        //{
        //    var date = invoice.InterchangeHeader.DateOfPreparation.ToString(DateFormat);
        //    var referenceNumber = invoice.InterchangeControlReference;
        //    var invoiceNumber = invoice.DocumentNumber;
        //    return string.Format(EDIFileNameFormat, date, invoiceNumber, referenceNumber);
        //}

        //private static string GenerateInvoiceFileName(SFTIInvoiceType invoice) 
        //{
        //    var date = invoice.IssueDate.Value.ToString(DateFormat);
        //    var invoiceNumber = invoice.ID.Value;
        //    return string.Format(SvefakturaFileNameFormat, date, invoiceNumber);
        //}

        //private static string GenerateInvoiceFileName(SFTIInvoiceList invoices) 
        //{
        //    var maxId = invoices.Invoices.Max(x => x.ID.Value);
        //    var minId = invoices.Invoices.Min(x => x.ID.Value);
        //    return string.Format(SvefakturaListFileNameFormat, minId, maxId, DateTime.Now.ToString(DateFormat));
        //}

        //private string UploadTextFileToFTP(string fileName, string fileContent) 
        //{
        //    try 
        //    {
        //        var ftp = GetFtpClientObject();
        //        var usePassiveFtp = WebService.UsePassiveFTP;
        //        var encoding = WebService.FTPCustomEncodingCodePage;
        //        var useBinaryTransfer = WebService.FTPUseBinaryTransfer;
        //        var response = ftp.UploadStringAsFile(fileName, fileContent, usePassiveFtp, encoding, useBinaryTransfer);
        //        var responseStatusDescription = response.StatusDescription;
        //        CheckFtpUploadStatusDescriptionForErrorMessages(responseStatusDescription);
        //        return responseStatusDescription;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw LogAndCreateException("SynologenService.UploadTextFileToFTP failed", ex);
        //    }
        //}

        //private void TrySaveContentToDisk(string fileName, string fileContent) 
        //{
        //    var encoding = WebService.FTPCustomEncodingCodePage;
        //    TrySaveContentToDisk(fileName, fileContent, encoding);
        //}

        //private void TrySaveContentToDisk(string fileName, string fileContent, Encoding encoding) 
        //{
        //    try 
        //    {
        //        var path = WebService.EDIFilesFolderPath;
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        var filePath = Path.Combine(path, fileName);
        //        File.AppendAllText(filePath, fileContent, encoding);
        //    }
        //    catch (Exception ex) 
        //    {
        //        TryLogErrorAndSendEmail("SynologenService.SaveContentToDisk failed: " + ex.Message);
        //    }
        //}

        //private static Ftp GetFtpClientObject() 
        //{
        //    var ftpUrl = WebService.FTPServerUrl;
        //    var ftpUserName = WebService.FTPUserName;
        //    var ftpPassword = WebService.FTPPassword;
        //    var credentials = new NetworkCredential { UserName = ftpUserName, Password = ftpPassword };
        //    return new Ftp(ftpUrl, credentials);
        //}

        //private static void CheckFtpUploadStatusDescriptionForErrorMessages(string statusDescription) 
        //{
        //    statusDescription = statusDescription.ToLower().Trim();
        //    if (!statusDescription.StartsWith(Ftp.FileTransferCompleteResponseCode)) 
        //    {
        //        throw new WebserviceException("Ftp transmission failed: " + statusDescription);
        //    }

        //    if (statusDescription.Contains(FtpFileUploadNotAccepted)) 
        //    {
        //        throw new WebserviceException("Ftp transmission reported EDI file was not accepted: " + statusDescription);
        //    }

        //    if (statusDescription.Contains(FtpFileUploadContainsError)) 
        //    {
        //        throw new WebserviceException("Ftp transmission reported EDI file contains errors: " + statusDescription);
        //    }
        //}

		#region To Invoice Conversion
        //private static Invoice GenerateEDIInvoice(IOrder order) 
        //{
        //    var ediSettings = GetEDISetting();
        //    var invoice = Convert.ToEDIInvoice(ediSettings, order);
        //    return invoice;
        //}
		#endregion

		#region Error Handling
		private Exception LogAndCreateException(string message, Exception ex) 
        {
			var exception = new WebserviceException(message, ex);
			LogMessage(LogType.Error, ex.ToString());
			if (_config.SendAdminEmailOnError) 
            {
				TrySendErrorEmail(exception.ToString());
			}

			return exception;
		}

		private void TrySendErrorEmail(string message) 
        {
			try 
            {
				var smtpClient = new SmtpClient(_config.SMTPServer);
				var mailMessage = new MailMessage();
				mailMessage.To.Add(_config.EmailAdminAddress);
				mailMessage.From = new MailAddress(_config.ErrorEmailSenderAddress);
				mailMessage.Subject = ServiceResources.resx.ServiceResources.ErrorEmailSubject;
				mailMessage.Body = message;
				smtpClient.Send(mailMessage);
			}
			catch{return;}
		}

		private void TryLogErrorAndSendEmail(string message) 
        {
			try 
            {
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
		    if (value == null)
		    {
		        return null;
		    }

		    var returnValue = new TType();
		    actionIfValueIsNotNull(returnValue, value);
			return returnValue;
		}
	}
}