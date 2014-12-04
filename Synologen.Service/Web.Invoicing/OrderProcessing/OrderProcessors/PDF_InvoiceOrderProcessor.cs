using System.IO;
using Spinit.Security.Password;
using Spinit.Services.Client;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Application.Web;
using Spinit.Wpc.Synologen.Reports.Invoicing;
using Synologen.Service.Web.Invoicing.Services;
using IFtpService = Synologen.Service.Web.Invoicing.Services.IFtpService;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PDF_InvoiceOrderProcessor : OrderProcessorBase
    {
        private readonly IInvoiceReportViewService _invoiceReportViewService;
        private readonly EmailClient2 _mailClient;

        public PDF_InvoiceOrderProcessor(ISqlProvider provider, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration)
            : base(provider, ftpService, mailService, fileService, orderProcessConfiguration)
        {
            ClientFactory.SetConfigurtion(ClientFactory.CreateConfiguration(
                      "http://services.spinit.se",
                      "SynologenSendUser",
                      "yM-28iB",
                      PasswordEncryptionType.Sha1,
                      "Utf-8")
              );
            _mailClient = ClientFactory.CreateEmail2Client();

            _invoiceReportViewService = new InvoiceReportViewService();
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
           
            foreach (var order in ordersToProcess)
            {   
                var invoiceOrderPdf = GetOrderInvoicePdf(order);
                SendMailWithInvoicePdf(order, invoiceOrderPdf);
            }
            
            return "Not Implemented";
        }

        private Stream GetOrderInvoicePdf(IOrder order)
        {
            var invoice = Provider.GetOrder(order.Id);
            var dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice);
            const string embeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.ReportDesign.Invoice.rdlc";
            var assembly = typeof(InvoiceCopyReport).Assembly;
            var reportResultsContentPdf = new PDFReportResult(assembly, embeddedReportFullName, dataSources).GetFileContents();

            return new MemoryStream(reportResultsContentPdf);
        }

        private void SendMailWithInvoicePdf(IOrder order, Stream invoiceOrderPdf)
        {
            var company = Provider.GetCompanyRow(order.CompanyId);
            var customerEmail = company.Email;

            var invoiceMonthDay = order.CreatedDate.ToString("dd-MM-yyyy");
            //TODO Uncomment this line before release to send invoice to real customer
            //var to = customerEmail;
            //var friendlyTo = customerEmail;
            var to = "sebastian.applerolsson@spinit.se";
            var friendlyTo = "sebastian.applerolsson@spinit.se";

            var from = "faktura@synologen.se";
            var friendlyFrom = "faktura@synologen.se";
            
            var errorAddress = "sebastian.applerolsson@spinit.se";
            var subject = string.Format("Faktura {0}", invoiceMonthDay);
            var body = "Test";
            var altBody = "Test";

            _mailClient.StartSequence();
            _mailClient.SendMailSequence(to, friendlyTo, from, friendlyFrom, errorAddress, subject, body, altBody, EmailPriority.Medium);
            _mailClient.SendAttachment(string.Format("{0}_{1}.pdf", order.Id, invoiceMonthDay), invoiceOrderPdf);
            _mailClient.StopSequence();
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