using System.Reflection;
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

		public PDFReportResult PDFReport(Assembly assembly, string embeddedReportFullNameSpaceName, params ReportDataSource[] dataSources)
		{
			return new PDFReportResult(assembly, embeddedReportFullNameSpaceName, dataSources);
		}

		public PDFReportResult PDFReportInAssemblyOf<TType>(string embeddedReportFullNameSpaceName, params ReportDataSource[] dataSources)
		{
			var assembly = typeof (TType).Assembly;
			return new PDFReportResult(assembly, embeddedReportFullNameSpaceName, dataSources);
		}
	}
}