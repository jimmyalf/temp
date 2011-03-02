using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
    public interface IFileReaderService
    {
        string[] ReadFileFromDisk(out string name);
    }
}
