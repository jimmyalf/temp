using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Spinit.Wpc.Synologen.Presentation.Application.Web
{
	public class PDFReportResult : FileResult
	{
		private readonly string _reportPathOnDisk;
		private readonly Assembly _reportAssembly;
		private readonly string _embeddedReportFullNameSpaceName;
		private readonly ReportDataSource[] _dataSources;
		public string RenderedContentType;

		public PDFReportResult(string reportPathOnDiskOnDisk, params ReportDataSource[] dataSources) : base("N/A")
		{
			_reportPathOnDisk = reportPathOnDiskOnDisk;
			_dataSources = dataSources;
		}

		public PDFReportResult(Assembly reportAssembly, string embeddedReportFullNameSpaceName, params ReportDataSource[] dataSources) : base("N/A")
		{
			_reportAssembly = reportAssembly;
			_embeddedReportFullNameSpaceName = embeddedReportFullNameSpaceName;
			_dataSources = dataSources;
		}

		private byte[] GetFileContents()
		{
			var localReport =  (String.IsNullOrEmpty(_reportPathOnDisk)) 
				? GetLocalReport(_reportAssembly,_embeddedReportFullNameSpaceName)
				: GetLocalReport(_reportPathOnDisk);
			foreach (var reportDataSource in _dataSources)
			{
				localReport.DataSources.Add(reportDataSource);	
			}
			return RenderReportData(localReport);
		}

		protected virtual LocalReport GetLocalReport(string reportPathOnDisk)
		{
			return new LocalReport {ReportPath = _reportPathOnDisk, EnableExternalImages = true};
		}
		protected virtual LocalReport GetLocalReport(Assembly assembly, string embeddedReportFullNameSpaceName)
		{
			var stream = assembly.GetManifestResourceStream(embeddedReportFullNameSpaceName);
			var localReport = new LocalReport();
			localReport.LoadReportDefinition(stream);
			return localReport;
		}

		protected virtual byte[] RenderReportData(LocalReport localReport)
		{
			Warning[] warnings;
			string[] streams;
			string encoding, fileNameExtension;
			var deviceInfo = GetDeviceInfo();
			return localReport.Render(
				"PDF" /*format*/, 
				deviceInfo /*deviceInfo*/, 
				out RenderedContentType,
				out encoding,
				out fileNameExtension,
				out streams,
				out warnings);
		}

		protected virtual string GetDeviceInfo()
		{
			//The DeviceInfo settings should be changed based on the reportType
			//http://msdn2.microsoft.com/en-us/library/ms155397.aspx
			//const string deviceInfo = "<DeviceInfo>" +
			//                          "  <OutputFormat>PDF</OutputFormat>" +
			//                          "  <PageWidth>210mm</PageWidth>" +
			//                          "  <PageHeight>297mm</PageHeight>" +
			//                          "  <MarginTop>20mm</MarginTop>" +
			//                          "  <MarginLeft>30mm</MarginLeft>" +
			//                          "  <MarginRight>20mm</MarginRight>" +
			//                          "  <MarginBottom>20mm</MarginBottom>" +
			//                          "</DeviceInfo>";
			return null;
		}

		protected override void WriteFile(HttpResponseBase response)
		{
			var fileContents = GetFileContents();
			response.ContentType = RenderedContentType;
			response.OutputStream.Write(fileContents, 0, fileContents.Length);
		}
	}
}