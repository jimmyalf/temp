using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Web;
using Spinit.Wpc.Synologen.Reports.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
    public class ReportController : ReportBaseController
    {
        private readonly IInvoiceReportViewService _invoiceReportViewService;
        private readonly ISqlProvider _sqlProvider;

        public ReportController(ISqlProvider sqlProvider, IInvoiceReportViewService invoiceReportViewService)
        {
            _sqlProvider = sqlProvider;
            _invoiceReportViewService = invoiceReportViewService;
        }

        public ActionResult InvoiceCopy(int id)
        {
            Order invoice = _sqlProvider.GetOrder(id);
            ReportDataSource[] dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice);
            const string EmbeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.InvoiceCopy.rdlc";
            return PDFReportInAssemblyOf<InvoiceCopyReport>(EmbeddedReportFullName, dataSources);
        }

        public ActionResult InvoiceCredit(int id)
        {
            Order invoice = _sqlProvider.GetOrder(id);

            // Invertera kostnader för att kunna kreditera fakturan
            foreach (var orderItem in invoice.OrderItems)
            {
                orderItem.DisplayTotalPrice *= -1;
                orderItem.SinglePrice *= -1;
            }

            invoice.InvoiceSumExcludingVAT *= -1;
            invoice.InvoiceSumIncludingVAT *= -1;

            ReportDataSource[] dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice);
            const string EmbeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.InvoiceCredit.rdlc";
            return PDFReportInAssemblyOf<InvoiceCopyReport>(EmbeddedReportFullName, dataSources);
        }

    }
}