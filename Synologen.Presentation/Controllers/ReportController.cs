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

		//public ActionResult Index(int? items)
		//{
		//    const string invoiceText = "Anmärkningar mot denna faktura skall göras inom 8 dgr för att godkännas.{NewLine}Vid betalning efter förfallodagen debiteras dröjsmålsränta med diskonto {PenaltyChargePercent}%.{NewLine}Påminnelseavgift 45 kronor.";
		//    const string invoiceFreeText = "Beställare: {CustomerName}\r\nBeställarens födelsedagsdatum: {BirthdayDate}";

		//    var invoiceReportItem = new InvoiceReport
		//    {
		//        Id = 5, 
		//        InvoiceFreeText = invoiceFreeText.ReplaceWith(new {CustomerName = "Adam Bertil", BirthdayDate = "2011-07-07"}), 
		//        LineExtensionsTotalAmount = "1234.55", 
		//        TotalTaxAmount = "225.55", 
		//        TaxInclusinveTotalAmount = "1755", 
		//        InvoiceFooterText = invoiceText.ReplaceWith(new {PenaltyChargePercent = "12.5", NewLine = "\r\n"})
		//    };
		//    invoiceReportItem.SetInvoiceRecipient(new Company
		//    {
		//        StreetName = "Datavägen 2", 
		//        PostBox = "Postbox 1234", 
		//        City = "Askim", 
		//        InvoiceCompanyName = "Spinit AB", 
		//        Zip = "43632", 
		//        Country = new Country
		//        {
		//            Name = "Sverige"
		//        }
		//    }, new Order
		//    {
		//        CompanyUnit = "Enhet 123"
		//    });
		//    var invoiceReport = new ReportDataSource("InvoiceReport", new List<InvoiceReport> {invoiceReportItem} );

		//    Func<int,InvoiceRow> generateInvoiceRow = (seed) => new InvoiceRow
		//    {
		//        Description = "Beskrivning 1", 
		//        Quantity = ((seed)%5+1).ToString(), 
		//        SinglePrice = "55,25", 
		//        RowAmount = "221"
		//    };
		//    var invoiceRows = new ReportDataSource("InvoiceRow", Sequence.Generate(generateInvoiceRow, items ?? 30));

			
		//    return PDFReport(
		//        "~/Areas/SynologenAdmin/Reports/InvoiceReport.rdlc", 
		//        invoiceReport, 
		//        invoiceRows
		//    );
		//}

		public ActionResult InvoiceCopy(int id)
		{
			var invoice = _sqlProvider.GetOrder(id);
			var dataSources = _invoiceReportViewService.GetInvoiceReportDataSources(invoice);
			const string reportUrl = "~/Areas/SynologenAdmin/Reports/InvoiceReport.rdlc";
			return PDFReport(reportUrl, dataSources);
		}
	}
}
