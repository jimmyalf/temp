using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Reports.Invoicing;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PDF_OrderProcessor : OrderProcessorBase
    {
        public PDF_OrderProcessor(ISqlProvider provider, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration)
            : base(provider, ftpService, mailService, fileService, orderProcessConfiguration)
        {
        }

        public override OrderProcessResult Process(IList<IOrder> ordersToProcess)
        {
            var result = new OrderProcessResult();
            if (!ordersToProcess.Any())
            {
                return result;
            }

            var ftpStatusMessage = SendEmailInvoicesWithPdf(ordersToProcess, out result);

            foreach (var order in ordersToProcess)
            {
                try
                {
                    ProcessOrder(order);
                    result.AddSentOrderId(order.Id);
                }
                catch (Exception ex)
                {
                    result.AddFailedOrderId(order.Id, ex);
                }
            }

            return result;
        }

        private string SendEmailInvoicesWithPdf(IList<IOrder> ordersToProcess, out OrderProcessResult result)
        {

            result = new OrderProcessResult();
            IInvoiceReportViewService invoiceReportViewService = new InvoiceReportViewService();
            foreach (var order in ordersToProcess)
            {   
                //TODO
                //Genereate PDF
                var invoice = Provider.GetOrder(order.Id);
                var dataSources = invoiceReportViewService.GetInvoiceReportDataSources(invoice);
                
                //TODO
                //Send mail with pdf

            }
            
            

            return "NotImplemented";
        }

        private void ProcessOrder(IOrder order)
        {
            UpdateOrderStatus(order.Id);
            Provider.AddOrderHistory(order.Id, "Fakturan har skickats med mail.");
        }

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.PDF_Invoicing;
        }
    }
}