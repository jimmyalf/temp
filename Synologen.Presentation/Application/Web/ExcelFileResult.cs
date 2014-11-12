using System.Web.Mvc;
using OfficeOpenXml;

namespace Spinit.Wpc.Synologen.Presentation.Application.Web
{
    public class ExcelFileResult : FileContentResult
    {
        public ExcelFileResult(ExcelPackage package, string fileName)
            : base(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = fileName;
        }
    }
}