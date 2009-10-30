using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.WebService{
	public class SynologenService : ISynologenService{
		private readonly SqlProvider provider;
		private const string FtpFileUploadNotAccepted = "ej accepterad";
		private const string FtpFileUploadContainsError = "felaktig";
		private const int NumberOfDecimalsUsedForRounding = 2;

		public SynologenService() {
			provider = new SqlProvider(ServiceLibrary.ConfigurationSettings.WebService.ConnectionString);
		}
		public SynologenService(string connectionString){
			provider = new SqlProvider(connectionString);
		}

		public List<OrderData> GetOrdersForInvoicing() {
			try{
				var statusIdFilter = ServiceLibrary.ConfigurationSettings.WebService.NewSaleStatusId;
				var invoicingMethodIdFilter = ServiceLibrary.ConfigurationSettings.WebService.InvoicingMethodIdFilter;
				var orders = provider.GetOrdersForInvoicing(statusIdFilter, invoicingMethodIdFilter, null);
				var returnList = ConvertOrderData(orders);
				var orderString = GetOrderIdsFromList(returnList);
				LogMessage(LogTypeData.Information, "Client fetched orders from WPC: "+orderString);
				return returnList;
			}
			catch(Exception ex) {
				throw LogAndCreateException("SynologenService.GetOrdersForInvoicing failed", ex);
			}
		}

		public void SetOrderInvoiceNumber(int orderId, long invoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT){
			try{
				var importedStatusId = ServiceLibrary.ConfigurationSettings.WebService.SaleStatusIdAfterSPCSImport;
				provider.SetOrderInvoiceNumber(orderId, invoiceNumber, importedStatusId, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				
				var orderHistoryMessage = GetVismaNewOrderAddedHistoryMessage(invoiceNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				provider.AddOrderHistory(orderId, orderHistoryMessage);
				var orderString = "OrderId: " + orderId + ", InvoiceNumber: " + invoiceNumber;
				LogMessage(LogTypeData.Information, "Client wrote back order invoice number [" + orderString + "]: ");
				var newStatusId = ServiceLibrary.ConfigurationSettings.WebService.SaleStatusIdAfterSPCSImport;
				provider.UpdateOrderStatus(newStatusId, orderId, 0, 0, 0, 0, 0);
			}
			catch(Exception ex) {
				throw LogAndCreateException("SynologenService.SetOrderInvoiceNumber failed", ex);
			}
		}

		public int LogMessage (LogTypeData logType, string message ) {
			int returnValue;
			try {
				switch (logType) {
					case LogTypeData.Error:
					if(ServiceLibrary.ConfigurationSettings.WebService.SendAdminEmailOnError) {
							TrySendErrorEmail(message);
						}
						break;
					case LogTypeData.Information:
						if(!ServiceLibrary.ConfigurationSettings.WebService.LogInformation)
							return 0;
						break;
					case LogTypeData.Other:
						if(!ServiceLibrary.ConfigurationSettings.WebService.LogOther)
							return 0;
						break;
					default:
						throw new ArgumentOutOfRangeException("logType");
				}
				returnValue = provider.AddLog((LogType) logType, message);
			}
			catch(Exception ex) {
				throw LogAndCreateException("SynologenService.LogMessage failed", ex);
			}
			return returnValue;
		}

		public List<long> GetOrdersToCheckForUpdates() {
			List<long> listOfOrders;
			try{
				var statusId = ServiceLibrary.ConfigurationSettings.WebService.SaleStatusIdAfterInvoicing;
				listOfOrders = provider.GetOrderInvoiceNumbers(statusId, null);
				var orderString = GetOrderIdsFromList(listOfOrders);
				LogMessage(LogTypeData.Information, "Client fetched order-invoice-id's from WPC : " + orderString);
			}
			catch (Exception ex) {
				throw LogAndCreateException("SynologenService.GetOrdersToCheckForUpdates failed", ex);
			}
			return listOfOrders;

		}

		public void UpdateOrderStatuses(InvoiceStatusData invoiceStatus) {
			if (invoiceStatus.InvoiceCanceled || invoiceStatus.InvoicePaymentCanceled) {
				var newStatusId = ServiceLibrary.ConfigurationSettings.WebService.InvoiceCancelledStatusId;
				provider.UpdateOrderStatus(newStatusId, 0, 0, 0, 0, 0, invoiceStatus.InvoiceNumber);
				var orderHistoryMessage = GetVismaOrderStatusUpdateHistoryMessage(invoiceStatus.InvoiceNumber);
				provider.AddOrderHistory(invoiceStatus.InvoiceNumber, orderHistoryMessage);
				return;
			}
			if (invoiceStatus.InvoiceIsPayed) {
				var newStatusId = ServiceLibrary.ConfigurationSettings.WebService.InvoicePayedToSynologenStatusId;
				provider.UpdateOrderStatus(newStatusId, 0, 0, 0, 0, 0, invoiceStatus.InvoiceNumber);
				var orderHistoryMessage = GetVismaOrderStatusUpdateHistoryMessage(invoiceStatus.InvoiceNumber);
				provider.AddOrderHistory(invoiceStatus.InvoiceNumber, orderHistoryMessage);
				return;
			}

		}

		/// <summary>
		/// Creates an invoice and uploads it as a text file to a configurable ftp-url.
		/// If successful it will change sale status id when transfer is completed
		/// </summary>
		/// <param name="orderId"></param>
		public void SendInvoiceEDI(int orderId) {
			try{
				var order = provider.GetOrder(orderId);
				var invoice = GenerateInvoice(order);
				var invoiceFileName = GenerateInvoiceFileName(invoice);
				if(ServiceLibrary.ConfigurationSettings.WebService.SaveEDIFileCopy) {
					TrySaveContentToDisk(invoiceFileName, invoice.Parse().ToString());
				}
				var ftpStatusMessage = UploadEDIFile(invoiceFileName,invoice.Parse().ToString());

				var newStatusId = ServiceLibrary.ConfigurationSettings.WebService.SaleStatusIdAfterInvoicing;
				provider.UpdateOrderStatus(newStatusId, orderId, 0, 0, 0, 0, 0);

				var orderHistoryMessage = GetInvoiceSentHistoryMessage(order.InvoiceNumber, ftpStatusMessage);
				provider.AddOrderHistory(orderId, orderHistoryMessage);

			}
			catch(Exception ex) {
				throw LogAndCreateException("SynologenService.SendInvoiceEDI failed [OrderId: "+orderId+"]", ex);
			}

		}

		/// <summary>
		/// Sends a regular text-email
		/// </summary>
		public void SendEmail(string from, string to, string subject, string message) {
			try {
				var smtpServerAddress = ServiceLibrary.ConfigurationSettings.WebService.SMTPServer;
				var smtpClient = new SmtpClient(smtpServerAddress);
				var mailMessage = new MailMessage(from, to, subject, message);
				smtpClient.Send(mailMessage);
			}
			catch(Exception ex) {
				throw new SynologenWebserviceException("Email could not be sent", ex);
			}
		}

		#region Helper methods

		private List<OrderData> ConvertOrderData(IEnumerable<IOrder> orderList) {
			var returnList = new List<OrderData>();
			try{
				foreach (var order in orderList) {
					var newOrder = new OrderData(order);
					newOrder.SellingShop = new ShopData(provider.GetShop(newOrder.SalesPersonShopId));
					newOrder.ContractCompany = new ContractCompanyData(provider.GetCompanyRow(newOrder.CompanyId));
					var orderItems = provider.GetIOrderItemsList(newOrder.Id, 0, null);
					newOrder.OrderItems = ConvertOrderItemData(orderItems);
					returnList.Add(newOrder);
				}
			}
			catch(Exception ex) {
				throw new SynologenWebserviceException("SynologenService.ConvertOrderData failed",ex);
			}
			return returnList;
		}

		private static List<OrderItemData> ConvertOrderItemData(IEnumerable<IOrderItem> orderItems) {
			var returnList = new List<OrderItemData>();
			foreach (var item in orderItems) {
				returnList.Add(new OrderItemData(item));	
			}
			return returnList;
		}

		private static string GetVismaNewOrderAddedHistoryMessage(long vismaOrderId, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {
			var message = Resources.ServiceResources.OrderAddedToVismaHistoryMessage;
			message = message.Replace("{0}", vismaOrderId.ToString());
			message = message.Replace("{1}", invoiceSumIncludingVAT.ToString());
			message = message.Replace("{2}", invoiceSumExcludingVAT.ToString());
			return message;
		}

		private static string GetVismaOrderStatusUpdateHistoryMessage(long vismaOrderId) {
			var message = Resources.ServiceResources.OrderStatusUpdatedHistoryMessage;
			message = message.Replace("{0}", vismaOrderId.ToString());
			return message;
		}

		private static string GetInvoiceSentHistoryMessage(long invoiceNumber, string ftpStatusMessage) {
			var message = Resources.ServiceResources.InvoiceSentHistoryMessage;
			message = message.Replace("{0}", invoiceNumber.ToString());
			message = message.Replace("{1}", ftpStatusMessage);
			return message;
		}

		private static string GetOrderIdsFromList(IEnumerable<OrderData> orders) {
			var returnString = String.Empty;
			foreach (var order in orders) {
				returnString += order.Id + ",";
			}
			return returnString.TrimEnd(',');
		}

		private static string GetOrderIdsFromList(IEnumerable<long> orderIds) {
			var returnString = String.Empty;
			foreach(var id in orderIds) {
				returnString += id + ",";
			}
			return returnString.TrimEnd(',');
		}

		private Exception LogAndCreateException(string message, Exception ex) {
			var exception = new SynologenWebserviceException(message, ex);
			LogMessage(LogTypeData.Error, ex.ToString());
			if(ServiceLibrary.ConfigurationSettings.WebService.SendAdminEmailOnError) {
				TrySendErrorEmail(exception.ToString());
			}
			return exception;
		}

		private static void TrySendErrorEmail(string message) {
			try {
				var smtpClient = new SmtpClient(ServiceLibrary.ConfigurationSettings.WebService.SMTPServer);
				var mailMessage = new MailMessage();
				mailMessage.To.Add(ServiceLibrary.ConfigurationSettings.WebService.AdminEmail);
				mailMessage.From = new MailAddress(ServiceLibrary.ConfigurationSettings.WebService.EmailSender);
				mailMessage.Subject = Resources.ServiceResources.ErrorEmailSubject;
				mailMessage.Body = message;
				smtpClient.Send(mailMessage);
			}
			catch{return;}
		}

		private void TryLogErrorAndSendEmail(string message) {
			try {
				LogMessage(LogTypeData.Error, message);
			}
			catch { return; }
		}

		private static EDIConversionSettings GetEDISetting() {
			return new EDIConversionSettings {
				BankGiro = ServiceLibrary.ConfigurationSettings.WebService.BankGiro,
				//InvoiceExpieryDate = DateTime.Now.AddDays(ServiceLibrary.ConfigurationSettings.WebService.InvoiceExpieryNumberOfDaysOffset),
				Postgiro = ServiceLibrary.ConfigurationSettings.WebService.Postgiro,
				//RecipientId = ServiceLibrary.ConfigurationSettings.WebService.EDIRecipientId,
				SenderId = ServiceLibrary.ConfigurationSettings.WebService.EDISenderId,
				VATAmount = ServiceLibrary.ConfigurationSettings.WebService.VATAmount,
				InvoiceCurrencyCode = ServiceLibrary.ConfigurationSettings.WebService.InvoiceCurrencyCode,
				NumberOfDecimalsUsedAtRounding = NumberOfDecimalsUsedForRounding
			};
		}

		private static string GenerateInvoiceFileName(Invoice invoice) {
			var fileFormat = "Synologen-{0}-{1}-{2}.txt";
			var date = invoice.InterchangeHeader.DateOfPreparation.ToString("yyyy-MM-dd");
			var referenceNumber = invoice.InterchangeControlReference;
			var invoiceNumber = invoice.DocumentNumber;
			return String.Format(fileFormat, date, invoiceNumber, referenceNumber);
		}

		private string UploadEDIFile(string fileName, string fileContent) {
			try {
				var ftp = GetFtpClientObject();
				var usePassiveFtp = ServiceLibrary.ConfigurationSettings.WebService.UsePassiveFTP;
				var encoding = ServiceLibrary.ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
				var useBinaryTransfer = ServiceLibrary.ConfigurationSettings.WebService.FTPUseBinaryTransfer;
				var response = ftp.UploadStringAsFile(fileName, fileContent, usePassiveFtp, encoding, useBinaryTransfer);
				var responseStatusDescription = response.StatusDescription;
				//response.Close();
				CheckFtpUploadStatusDescriptionForErrorMessages(responseStatusDescription);
				return responseStatusDescription;
			}
			catch (Exception ex){
				throw LogAndCreateException("SynologenService.UploadEDIFile failed", ex);
			}
		}

		private void TrySaveContentToDisk(string fileName, string fileContent) {
			try {
				var path = ServiceLibrary.ConfigurationSettings.WebService.EDIFilesFolderPath;
				if(!Directory.Exists(path)) {Directory.CreateDirectory(path); }
				var filePath = Path.Combine(path, fileName);
				var encoding = ServiceLibrary.ConfigurationSettings.WebService.FTPCustomEncodingCodePage;
				File.AppendAllText(filePath, fileContent, encoding);
			}
			catch(Exception ex) {
				TryLogErrorAndSendEmail("SynologenService.SaveContentToDisk failed: " +ex.Message);
			}
		}

		private static Ftp GetFtpClientObject() {
			var ftpUrl = ServiceLibrary.ConfigurationSettings.WebService.FTPServerUrl;
			var ftpUserName = ServiceLibrary.ConfigurationSettings.WebService.FTPUserName;
			var ftpPassword = ServiceLibrary.ConfigurationSettings.WebService.FTPPassword;
			var credentials = new NetworkCredential { UserName = ftpUserName, Password = ftpPassword };
			return new Ftp(ftpUrl, credentials);
		}

		private static void CheckFtpUploadStatusDescriptionForErrorMessages(string statusDescription) {
			statusDescription = statusDescription.ToLower().Trim();
			if (!statusDescription.StartsWith(Ftp.FileTransferCompleteResponseCode)) {
				throw new SynologenWebserviceException("Ftp transmission failed: " + statusDescription);
			}
			if (statusDescription.Contains(FtpFileUploadNotAccepted)) {
				throw new SynologenWebserviceException("Ftp transmission reported EDI file was not accepted: " + statusDescription);
			}
			if (statusDescription.Contains(FtpFileUploadContainsError)) {
				throw new SynologenWebserviceException("Ftp transmission reported EDI file contains errors: " + statusDescription);
			}
		}

		private Invoice GenerateInvoice(Order order) {
			var orderItems = provider.GetIOrderItemsList(order.Id, 0, null);
			var company = provider.GetCompanyRow(order.CompanyId);
			var shop = provider.GetShop(order.SalesPersonShopId);
			var ediSettings = GetEDISetting();
			var invoice = Utility.Convert.ToEDIInvoice(ediSettings, order, orderItems, company, shop);
			return invoice;
		}

		#endregion

	}
}