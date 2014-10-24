using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Business.Extensions;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Validators;

namespace Spinit.Wpc.Synologen.ServiceLibrary
{
	public partial class SynologenService : ISynologenService
    {
		private readonly ISqlProvider provider;
	    private EBrevSvefakturaBuilder _eBrevSvefakturaBuilder;

	    public SynologenService() 
        {
			provider = new SqlProvider(ConfigurationSettings.WebService.ConnectionString);
            var settings = GetSvefakturaSettings();
            _eBrevSvefakturaBuilder = new EBrevSvefakturaBuilder(new SvefakturaFormatter(), settings, new EBrev_SvefakturaBuilderValidator());
		}

		public SynologenService(ISqlProvider sqlProvider) : this()
        {
			provider = sqlProvider;
		}

		public List<Order> GetOrdersForInvoicing()
        {
			try
            {
				var statusIdFilter = ConfigurationSettings.WebService.NewSaleStatusId;
				var invoicingMethodIdFilter = ConfigurationSettings.WebService.InvoicingMethodIdFilter;
				var orders = provider.GetOrdersForInvoicing(statusIdFilter, invoicingMethodIdFilter, null);
				var orderString = orders.ToFormattedString(x => x.Id.ToString(), ", ");
				LogMessage(LogType.Information, "Client fetched orders from WPC: " + orderString);
				return orders;
			}
			catch (Exception ex)
            {
				throw LogAndCreateException("SynologenService.GetOrdersForInvoicing failed", ex);
			}
		}

		public void SetOrderInvoiceNumber(int orderId, long invoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT)
        {
			try
            {
				var importedStatusId = ConfigurationSettings.WebService.SaleStatusIdAfterSPCSImport;
				provider.SetOrderInvoiceNumber(orderId, invoiceNumber, importedStatusId, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				
				var orderHistoryMessage = GetVismaNewOrderAddedHistoryMessage(invoiceNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				provider.AddOrderHistory(orderId, orderHistoryMessage);
				var orderString = "OrderId: " + orderId + ", InvoiceNumber: " + invoiceNumber;
				LogMessage(LogType.Information, "Client wrote back order invoice number [" + orderString + "]: ");
				var newStatusId = ConfigurationSettings.WebService.SaleStatusIdAfterSPCSImport;
				provider.UpdateOrderStatus(newStatusId, orderId, 0, 0, 0, 0, 0);
			}
			catch (Exception ex)
            {
				throw LogAndCreateException("SynologenService.SetOrderInvoiceNumber failed", ex);
			}
		}

		public int LogMessage (LogType logType, string message )
        {
			int returnValue;
			try 
            {
				switch (logType) 
                {
					case LogType.Error:
						if (ConfigurationSettings.WebService.SendAdminEmailOnError) 
                        {
							TrySendErrorEmail(message);
						}

						break;
					case LogType.Information:
                        if (!ConfigurationSettings.WebService.LogInformation)
                        {
                            return 0;
                        }

                        break;
					case LogType.Other:
                        if (!ConfigurationSettings.WebService.LogOther)
                        {
                            return 0;
                        }

                        break;
					default:
						throw new ArgumentOutOfRangeException("logType");
				}

				returnValue = provider.AddLog(logType, message);
			}
			catch (Exception ex) 
            {
				throw LogAndCreateException("SynologenService.LogMessage failed", ex);
			}

			return returnValue;
		}

		public List<long> GetOrdersToCheckForUpdates()
        {
			List<long> listOfOrders;
			try
            {
				var statusId = ConfigurationSettings.WebService.SaleStatusIdAfterInvoicing;
				listOfOrders = provider.GetOrderInvoiceNumbers(statusId, null);
				var orderString = listOfOrders.ToFormattedString(", ");
				LogMessage(LogType.Information, "Client fetched order-invoice-id's from WPC : " + orderString);
			}
			catch (Exception ex) 
            {
				throw LogAndCreateException("SynologenService.GetOrdersToCheckForUpdates failed", ex);
			}

			return listOfOrders;
		}

		public void UpdateOrderStatuses(long invoiceNumber, bool invoiceIsCanceled, bool invoiceIsPayed)
        {
			if (invoiceIsCanceled) 
            {
				var newStatusId = ConfigurationSettings.WebService.InvoiceCancelledStatusId;
				provider.UpdateOrderStatus(newStatusId, 0, 0, 0, 0, 0, invoiceNumber);
				var orderHistoryMessage = GetVismaOrderStatusUpdateHistoryMessage(invoiceNumber);
				provider.AddOrderHistory(invoiceNumber, orderHistoryMessage);
				return;
			}

			if (invoiceIsPayed) 
            {
				var newStatusId = ConfigurationSettings.WebService.InvoicePayedToSynologenStatusId;
				provider.UpdateOrderStatus(newStatusId, 0, 0, 0, 0, 0, invoiceNumber);
				var orderHistoryMessage = GetVismaOrderStatusUpdateHistoryMessage(invoiceNumber);
				provider.AddOrderHistory(invoiceNumber, orderHistoryMessage);
				return;
			}
		}

		/// <summary>
		/// Creates a number of invoices and uploads them as a text file to a configurable ftp-url.
		/// If successful it will change sale status id for each order when transfer is completed
		/// </summary>
		/// <param name="orderIds">List of order id's to perform invoicing on</param>
		/// <param name="reportEmailAddress">Email address to where a status report will be send when invoicing has been processed</param>
		public void SendInvoices(List<int> orderIds, string reportEmailAddress)
        {
		    var sentOrderIds = new List<int>();
			try
            {
				var orderList = provider.GetOrders(orderIds);
				var ediOrders = orderList
                    .Where(x => (InvoicingMethod)x.ContractCompany.InvoicingMethodId == InvoicingMethod.EDI)
                    .ToList();
				if(ediOrders.Any())
                {
					foreach (var order in ediOrders)
                    {
						var ftpStatusMessage = SendEDIInvoice(order);
						sentOrderIds.Add(order.Id);

                        var newStatusId = ConfigurationSettings.WebService.SaleStatusIdAfterInvoicing;
                        provider.UpdateOrderStatus(newStatusId, order.Id, 0, 0, 0, 0, 0);

                        var orderHistoryMessage = GetInvoiceSentHistoryMessage(order.InvoiceNumber, ftpStatusMessage);
                        provider.AddOrderHistory(order.Id, orderHistoryMessage);
					}
				}

				var letterOrders = orderList
                    .Where(x => (InvoicingMethod)x.ContractCompany.InvoicingMethodId == InvoicingMethod.LetterInvoice)
                    .ToList();
				if(letterOrders.Any())
                {
					var ftpStatusMessage = SendLetterInvoices(letterOrders);
					sentOrderIds.AddRange(letterOrders.Select(x=>x.Id));
					foreach (var order in letterOrders)
                    {
						var newStatusId = ConfigurationSettings.WebService.SaleStatusIdAfterInvoicing;
						provider.UpdateOrderStatus(newStatusId, order.Id, 0, 0, 0, 0, 0);

						var orderHistoryMessage = GetInvoiceSentHistoryMessage(order.InvoiceNumber, ftpStatusMessage);
						provider.AddOrderHistory(order.Id, orderHistoryMessage);
					}
				}
			}
			catch (Exception ex) 
            {
				throw LogAndCreateException("SynologenService.SendInvoices failed ", ex);
			}
			finally
			{
				SendStatusReportAfterBatchInvoice(orderIds, sentOrderIds, reportEmailAddress);
				var invoiceDateTime = GetDateTime();
				sentOrderIds.ForEach(orderId => provider.SetOrderInvoiceDate(orderId, invoiceDateTime));
			}
		}

		protected virtual DateTime GetDateTime()
		{
			return DateTime.Now;
		}

		/// <summary>
		/// Sends a regular text-email
		/// </summary>
		public void SendEmail(string from, string to, string subject, string message) 
        {
			try 
            {
				var smtpServerAddress = ConfigurationSettings.WebService.SMTPServer;
				var smtpClient = new SmtpClient(smtpServerAddress);
				var mailMessage = new MailMessage(from, to, subject, message);
				smtpClient.Send(mailMessage);
			}
			catch (Exception ex) 
            {
				throw new WebserviceException("Email could not be sent", ex);
			}
		}
	}
}