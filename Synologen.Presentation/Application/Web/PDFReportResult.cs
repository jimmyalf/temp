using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Spinit.Wpc.Synologen.Reports.Rendering;

namespace Spinit.Wpc.Synologen.Presentation.Application.Web
{
	public class PDFReportResult : FileResult
	{
	    private readonly ReportRenderer _renderer;
        public string RenderedContentType;

		public PDFReportResult(string reportPathOnDiskOnDisk, params ReportDataSource[] dataSources) : base("N/A")
		{
            _renderer = new ReportRenderer(reportPathOnDiskOnDisk, dataSources);
		}

		public PDFReportResult(Assembly reportAssembly, string embeddedReportFullNameSpaceName, params ReportDataSource[] dataSources) : base("N/A")
		{
            _renderer = new ReportRenderer(reportAssembly, embeddedReportFullNameSpaceName, dataSources);
		}

		public byte[] GetFileContents()
		{
		    return _renderer.GetFileContents();
		}

		protected override void WriteFile(HttpResponseBase response)
		{
			var fileContents = GetFileContents();
		    response.ContentType = _renderer.RenderedContentType;
			response.OutputStream.Write(fileContents, 0, fileContents.Length);
		}
	}
}