using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using OfficeOpenXml;

namespace Spinit.Wpc.Synologen.Presentation.Application.Web
{
    public class ExcelFileResult : FileContentResult
    {
        private Dictionary<char, char> _characterReplacementMap;

        public ExcelFileResult(ExcelPackage package, string fileName)
            : base(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            _characterReplacementMap = new Dictionary<char, char>()
            {
                {'å', 'a'},
                {'Å', 'A'},
                {'ä', 'a'},
                {'Ä', 'A'},
                {'ö', 'o'},
                {'Ö', 'O'},
                {'é', 'e'},
                {'E', 'e'}
            };
            FileDownloadName = CleanFilename(fileName);
        }

        // Implemented for IE
        protected string CleanFilename(string fileName)
        {
            // Should probably be done better to use regex with an allowed-only expression to filter out not allowed characters
            return _characterReplacementMap.Keys.Aggregate(fileName, (current, key) => current.Replace(key, _characterReplacementMap[key]));
        }
    }
}