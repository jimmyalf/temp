using System.Web.Mvc;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Web;
using Spinit.Wpc.Synologen.Reports.Models;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public class ReportController : ReportBaseController
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
			const string embeddedReportFullName = "Spinit.Wpc.Synologen.Reports.Invoicing.InvoiceCopy.rdlc";
			return PDFReportInAssemblyOf<InvoiceCopyReport>(embeddedReportFullName, dataSources);
		}
	}
}
