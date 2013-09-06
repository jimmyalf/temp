using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public abstract class OrderProcessorBase : IOrderProcessor
    {
        protected IOrderProcessConfiguration OrderProcessConfiguration { get; set; }
        protected IFileService FileService { get; set; }
        protected IFtpService FtpService { get; set; }
        protected IMailService MailService { get; set; }
        protected ISqlProvider Provider { get; set; }
        protected const int NumberOfDecimalsUsedForRounding = 2;
        protected const string DateFormat = "yyyy-MM-dd";

        protected OrderProcessorBase(ISqlProvider provider, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration)
        {
            OrderProcessConfiguration = orderProcessConfiguration;
            FileService = fileService;
            FtpService = ftpService;
            MailService = mailService;
            Provider = provider;
        }

        public abstract OrderProcessResult Process(IList<IOrder> ordersToProcess);

        public abstract bool IHandle(InvoicingMethod method);

        public virtual IList<IOrder> FilterOrdersMatchingInvoiceType(IList<IOrder> orders)
        {
            return orders
                .Where(x => IHandle((InvoicingMethod)x.ContractCompany.InvoicingMethodId))
                .ToList();
        }

        public virtual IList<IOrder> FilterOrdersMatchingInvoiceType(IList<Order> orders)
        {
            return FilterOrdersMatchingInvoiceType(orders.Cast<IOrder>().ToList());
        }

        protected string GetInvoiceSentHistoryMessage(long invoiceNumber, string ftpStatusMessage)
        {
            var message = ServiceResources.resx.ServiceResources.InvoiceSentHistoryMessage;
            message = message.Replace("{0}", invoiceNumber.ToString());
            message = message.Replace("{1}", ftpStatusMessage);
            return message;
        }

        protected void UpdateOrderStatus(int orderId)
        {
            var newStatusId = OrderProcessConfiguration.SaleStatusIdAfterInvoicing;
            Provider.UpdateOrderStatus(newStatusId, orderId, 0, 0, 0, 0, 0);
        }

        protected void AddOrderHistory(int orderId, long invoiceNumber, string ftpStatusMessage)
        {
            var orderHistoryMessage = GetInvoiceSentHistoryMessage(invoiceNumber, ftpStatusMessage);
            Provider.AddOrderHistory(orderId, orderHistoryMessage);
        }

        protected void TrySaveContentToDisk(string fileName, string fileContent)
        {
            FileService.TrySaveContentToDisk(fileName, fileContent);
        }

        protected void TrySaveContentToDisk(string fileName, string fileContent, Encoding encoding)
        {
            try
            {
                FileService.TrySaveContentToDisk(fileName, fileContent, encoding);
            }
            catch (Exception ex)
            {
                TryLogErrorAndSendEmail("SynologenService.SaveContentToDisk failed: " + ex.Message);
            }
        }

        protected void TryLogErrorAndSendEmail(string message)
        {
            try
            {
                LogMessage(LogType.Error, message);
            }
            catch { return; }
        }

        protected int LogMessage(LogType logType, string message)
        {
            int returnValue;
            try
            {
                switch (logType)
                {
                    case LogType.Error:
                        if (OrderProcessConfiguration.SendAdminEmailOnError)
                        {
                            TrySendErrorEmail(message);
                        }

                        break;
                    case LogType.Information:
                        if (!OrderProcessConfiguration.LogInformation)
                        {
                            return 0;
                        }

                        break;
                    case LogType.Other:
                        if (!OrderProcessConfiguration.LogOther)
                        {
                            return 0;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException("logType");
                }

                returnValue = Provider.AddLog(logType, message);
            }
            catch (Exception ex)
            {
                throw LogAndCreateException("SynologenService.LogMessage failed", ex);
            }

            return returnValue;
        }

        protected Exception LogAndCreateException(string message, Exception ex)
        {
            var exception = new WebserviceException(message, ex);
            LogMessage(LogType.Error, ex.ToString());
            if (OrderProcessConfiguration.SendAdminEmailOnError)
            {
                TrySendErrorEmail(exception.ToString());
            }

            return exception;
        }

        protected void TrySendErrorEmail(string message)
        {
            try
            {
                MailService.SendMessage(message);
            }
            catch { return; }
        }

        protected string UploadTextFileToFTP(string fileName, string fileContent, NetworkCredential credential)
        {
            try
            {
                return FtpService.UploadTextFileToFTP(fileName, fileContent, credential);
            }
            catch (Exception ex)
            {
                throw LogAndCreateException("SynologenService.UploadTextFileToFTP failed", ex);
            }
        }
    }
}