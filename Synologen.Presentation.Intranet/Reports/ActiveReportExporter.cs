using System.Drawing.Printing;
using System.IO;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Pdf.Section;


namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
	public static class  ActiveReportExtensions
	{
        public static MemoryStream ToPdfStream(this SectionReport report)
        {
            report.PageSettings.PaperKind = PaperKind.Custom;
            report.Document.Printer.PrinterName = string.Empty;
            report.Run();
            var pdfExport = new PdfExport();
            var stream = new MemoryStream();
            pdfExport.Export(report.Document, stream);
            stream.Position = 0;
            return stream;
        }
	}
}