using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Client.BGCTaskRunner.App.Services
{
	public class BGFileIOService : IFileIOService
	{
		private readonly Encoding TextEncoding = System.Text.Encoding.GetEncoding(858);

		public void WriteFile(string filePath, string contents) 
		{
			File.WriteAllText(filePath, contents);
		}
		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

	    public string[] ReadFile(string filePath)
	    {
	        return File.ReadAllLines(filePath, TextEncoding);
	    }

	    public IEnumerable<string> GetReceivedFileNames(string folderPath)
	    {
            var directoryInfo = new DirectoryInfo(folderPath);
            var files = directoryInfo.GetFiles();
	        return files.Select(x => x.Name).ToList();
	    }

	    public void MoveFile(string source, string destination)
	    {
	        File.Move(source, destination);
	    }
	}
}