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
using Synologen.Service.Web.Invoicing.ConfigurationSettings;
using Synologen.Service.Web.Invoicing.OrderProcessing;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing
{
	public partial class SynologenService : ISynologenService
    {
		private readonly ISqlProvider _provider;
	    private readonly IOrderProcessorFactory _orderProcessorFactory;
	    private readonly WebServiceConfiguration _config;
	    private readonly MailService _mailService;

	    public SynologenService() : this(new SqlProvider(new WebServiceConfiguration().ConnectionString)){ }

		public SynologenService(ISqlProvider sqlProvider)
        {
			_provider = sqlProvider;
		    _config = new WebServiceConfiguration();
            _mailService = new MailService(_config);
		    _orderProcessorFactory = CreateOrderProcessorFactory();
        }

        protected IOrderProcessorFactory CreateOrderProcessorFactory()
        {
            var ftpService = new FtpService(_config);
            var fileService = new FileService(_config);
            var svefakturaSettings = Settings.GetSvefakturaSettings();
            var ediSettings = Settings.GetEDISetting();
            var pdfOrderInvoiceSettings = Settings.GetPDF_OrderInvoiceSettings();
            return new OrderProcessorFactory(_provider, ediSettings, svefakturaSettings, pdfOrderInvoiceSettings, ftpService, fileService, _mailService, _config);
        }

		public List<Order> GetOrdersForInvoicing()
        {
			try
            {
				var statusIdFilter = _config.NewSaleStatusId;
				var invoicingMethodIdFilter = _config.InvoicingMethodIdFilter;
				var orders = _provider.GetOrdersForInvoicing(statusIdFilter, invoicingMethodIdFilter, null);
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
				var importedStatusId = _config.SaleStatusIdAfterSPCSImport;
				_provider.SetOrderInvoiceNumber(orderId, invoiceNumber, importedStatusId, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				
				var orderHistoryMessage = GetVismaNewOrderAddedHistoryMessage(invoiceNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				_provider.AddOrderHistory(orderId, orderHistoryMessage);
				var orderString = "OrderId: " + orderId + ", InvoiceNumber: " + invoiceNumber;
				LogMessage(LogType.Information, "Client wrote back order invoice number [" + orderString + "]: ");
				var newStatusId = _config.SaleStatusIdAfterSPCSImport;
				_provider.UpdateOrderStatus(newStatusId, orderId, 0, 0, 0, 0, 0);
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
						if (_config.SendAdminEmailOnError) 
                        {
							TrySendErrorEmail(message);
						}

						break;
					case LogType.Information:
                        if (!_config.LogInformation)
                        {
                            return 0;
                        }

                        break;
					case LogType.Other:
                        if (!_config.LogOther)
                        {
                            return 0;
                        }

                        break;
					default:
						throw new ArgumentOutOfRangeException("logType");
				}

				returnValue = _provider.AddLog(logType, message);
			}
			catch (Exception ex) 
            {
				throw LogAndCreateException("SynologenService.LogMessage failed", ex, true);
			}

			return returnValue;
		}

		public List<long> GetOrdersToCheckForUpdates()
        {
			List<long> listOfOrders;
			try
            {
				var statusId = _config.SaleStatusIdAfterInvoicing;
				listOfOrders = _provider.GetOrderInvoiceNumbers(statusId, null);
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
				var newStatusId = _config.InvoiceCancelledStatusId;
				_provider.UpdateOrderStatus(newStatusId, 0, 0, 0, 0, 0, invoiceNumber);
				var orderHistoryMessage = GetVismaOrderStatusUpdateHistoryMessage(invoiceNumber);
				_provider.AddOrderHistory(invoiceNumber, orderHistoryMessage);
				return;
			}

			if (invoiceIsPayed) 
            {
				var newStatusId = _config.InvoicePayedToSynologenStatusId;
				_provider.UpdateOrderStatus(newStatusId, 0, 0, 0, 0, 0, invoiceNumber);
				var orderHistoryMessage = GetVismaOrderStatusUpdateHistoryMessage(invoiceNumber);
				_provider.AddOrderHistory(invoiceNumber, orderHistoryMessage);
				return;
			}
		}

		/// <summary>
		/// Creates a number of invoices and uploads them as files to a configurable ftp-url.
		/// If successful it will change sale status id for each order when transfer is completed
		/// </summary>
		/// <param name="orderIds">List of order id's to perform invoicing on</param>
		/// <param name="reportEmailAddress">Email address to where a status report will be send when invoicing has been processed</param>
		public void SendInvoices(List<int> orderIds, string reportEmailAddress)
		{
		    var results = new OrderProcessResultList();
			try
            {

                var ordersToProcess = _provider.GetOrders(orderIds);

                foreach (var invoicingMethod in Spinit.Wpc.Synologen.Core.Extensions.EnumExtensions.Enumerate<InvoicingMethod>())
                {
                    var processor = _orderProcessorFactory.GetOrderProcessorFor(invoicingMethod);
                    var orders = processor.FilterOrdersMatchingInvoiceType(ordersToProcess);
                    var result = processor.Process(orders);                    
                    results.Add(result);
                }
			}
			catch (Exception ex) 
            {
				throw LogAndCreateException("SynologenService.SendInvoices failed ", ex);
			}
			finally
			{
                SendStatusReportAfterBatchInvoice(results.Merge(), reportEmailAddress);
				var invoiceDateTime = GetDateTime();
			    foreach (var orderId in results.GetSentOrderIds())
			    {
                    _provider.SetOrderInvoiceDate(orderId, invoiceDateTime);
			    }
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
				var smtpServerAddress = _config.SMTPServer;
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