using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Application.Web;
using Spinit.Wpc.Synologen.Reports.Invoicing;

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
            var invoice = _sqlProvider.GetOrder(id);
            var dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice);
            const string EmbeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.ReportDesign.InvoiceCopy.rdlc";
            return PDFReportInAssemblyOf<InvoiceCopyReport>(EmbeddedReportFullName, dataSources);
        }

        public ActionResult InvoiceCredit(int id, string creditInvoiceNumber)
        {
            var invoice = _sqlProvider.GetOrder(id);
            var dataSources = _invoiceReportViewService.GetCreditInvoiceReportDataSources(invoice, creditInvoiceNumber);
            const string EmbeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.ReportDesign.InvoiceCredit.rdlc";
            return PDFReportInAssemblyOf<InvoiceCreditReport>(EmbeddedReportFullName, dataSources);
        }

    }
}