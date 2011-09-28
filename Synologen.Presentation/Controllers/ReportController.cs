using System.Web.Mvc;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Web;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class ReportController : SynologenController
	{
		private readonly ISqlProvider _sqlProvider;
		private readonly IInvoiceReportViewService _invoiceReportViewService;

		public ReportController(ISqlProvider sqlProvider, IInvoiceReportViewService invoiceReportViewService)
		{
			_sqlProvider = sqlProvider;
			_invoiceReportViewService = invoiceReportViewService;
		}

		public ActionResult InvoiceCopy(int id)
		{
			var invoice = _sqlProvider.GetOrder(id);
			var dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice);
			const string reportUrl = "~/Areas/SynologenAdmin/Reports/InvoiceReport.rdlc";
			return PDFReport(reportUrl, dataSources);
		}
	}
}
