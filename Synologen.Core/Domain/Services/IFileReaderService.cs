using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
    public interface IFileReaderService
    {
        string[] ReadFileFromDisk(string name);
        IEnumerable<string> GetFileNames();
        IEnumerable<FileSection> GetSections(string[] strings);
        void MoveFile(string fileName);
    }
}
