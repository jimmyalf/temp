using System.IO;
using Spinit.Security.Password;
using Spinit.Services.Client;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Reports.Invoicing;
using Spinit.Wpc.Synologen.Reports.Rendering;
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
        private readonly I_PDF_OrderInvoiceConversionSettings _settings;
        public PDF_InvoiceOrderProcessor(ISqlProvider provider, I_PDF_OrderInvoiceConversionSettings pdfInvoiceOrderSettings, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration)
            : base(provider, ftpService, mailService, fileService, orderProcessConfiguration)
        {
            _settings = pdfInvoiceOrderSettings;

            ClientFactory.SetConfigurtion(ClientFactory.CreateConfiguration(
                      _settings.EmailSpinitServiceAddress,
                      _settings.EmailSpinitServiceSendUser,
                      _settings.EmailSpinitServicePassword,
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

            SendEmailInvoicesWithPdf(ordersToProcess, out result);
           
            return result;
        }

        private void SendEmailInvoicesWithPdf(IList<IOrder> ordersToProcess, out OrderProcessResult result)
        {
            result = new OrderProcessResult();
            
            foreach (var order in ordersToProcess)
            {
                var emailId = 0;
                var logMessage = string.Empty;
                try
                {
                    var invoiceOrderPdf = GetOrderInvoicePdf(order);
                    emailId = SendMailWithInvoicePdf(order, invoiceOrderPdf);

                    result.AddSentOrderId(order.Id);
                    logMessage = string.Format("{0}: Faktura {1} har skickats som PDF", DateTime.Now.ToShortTimeString(), order.InvoiceNumber);

                    UpdateOrderStatus(order.Id);
                    AddOrderHistory(order.Id, order.InvoiceNumber, logMessage, "Epost");
                }
                catch (Exception exception)
                {
                    logMessage = string.Format("{0}: SynologenService.SendInvoice failed to send invoice [OrderId: {1}, MailId: {2}]", DateTime.Now.ToShortTimeString(), order.Id, emailId);
                    result.AddFailedOrderId(order.Id, exception);
                    LogAndCreateException(logMessage, exception);
                }
            }
        }

        private Stream GetOrderInvoicePdf(IOrder order)
        {
            var invoice = Provider.GetOrder(order.Id);
            var dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice, PDF_InvoiceType.Mail);
            const string embeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.ReportDesign.Invoice.rdlc";
            var assembly = typeof(InvoiceCopyReport).Assembly;
            var reportResultsContentPdf = new ReportRenderer(assembly, embeddedReportFullName, dataSources).GetFileContents();

            return new MemoryStream(reportResultsContentPdf);
        }

        private int SendMailWithInvoicePdf(IOrder order, Stream invoiceOrderPdf)
        {
            var company = Provider.GetCompanyRow(order.CompanyId);
            var customerEmail = company.Email;
            var invoiceDate = order.CreatedDate.ToString("dd-MM-yyyy");

            var to = _settings.PDF_OrderInvoiceDebugMode ? _settings.EmailAdminAddress : customerEmail;
            var friendlyTo = _settings.PDF_OrderInvoiceDebugMode ? _settings.EmailAdminAddress : customerEmail;
            var from = _settings.EmailSynologenInvoiceSender;
            var friendlyFrom = _settings.EmailSynologenInvoiceSender;
            var errorAddress = _settings.EmailAdminAddress;
            var subject = string.Format("Faktura Synologen {0}", order.InvoiceNumber);
            const string bodyText = "Hej,<br/>" +
                                    "Bifogat finner ni faktura gällande arbetsglasögon.<br/><br/>" +
                                    "Vänliga hälsningar<br/>" +
                                    "Synologen";
            const string body = bodyText;
            const string altBody = bodyText;

            _mailClient.StartSequence();
            var emailId = _mailClient.SendMailSequence(to, friendlyTo, from, friendlyFrom, errorAddress, subject, body, altBody, EmailPriority.Medium);
            _mailClient.SendAttachment(string.Format("{0}_{1}.pdf", order.InvoiceNumber, invoiceDate), invoiceOrderPdf);
            _mailClient.StopSequence();

            return emailId;
        }

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.PDF_Invoicing;
        }
    }
}