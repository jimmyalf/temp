using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFileIOService
	{
		void WriteFile(string filePath, string contents);
		bool FileExists(string filePath);
	    string[] ReadFile(string filePath);
        int GetNumberOfReceivedFiles(string folderPath);
        IEnumerable<string> GetReceivedFileNames(string folderPath);
	}
}