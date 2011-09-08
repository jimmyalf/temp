using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Spinit.Wpc.Synologen.Presentation.Application.Web
{
	public class SynologenController : Controller
	{
		public PDFReportResult PDFReport(string urlToReport, params ReportDataSource[] dataSources)
		{
			var pathOnDisk = Server.MapPath(urlToReport);
			return new PDFReportResult(pathOnDisk, dataSources);
		}
	}
}