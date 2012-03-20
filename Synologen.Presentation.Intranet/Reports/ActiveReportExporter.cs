using System.Drawing.Printing;
using System.IO;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
	public static class  ActiveReportExtensions
	{
		public static MemoryStream ToPdfStream(this ActiveReport report)
		{
			report.PageSettings.PaperKind = PaperKind.Custom;
            report.Run();
        	var pdfExport = new PdfExport();
        	var stream = new MemoryStream();
        	pdfExport.Export(report.Document, stream);
        	stream.Position = 0;
        	return stream;			
		}
	}
}