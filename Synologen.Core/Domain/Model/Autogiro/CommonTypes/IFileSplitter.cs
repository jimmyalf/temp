using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
    public interface IFileSplitter
    {
        DateTime GetDateFromName(string name);
        bool FileNameOk(string name, string customerNumber, string productCode);
        IEnumerable<FileSection> GetSections(string[] file);
    }
}
