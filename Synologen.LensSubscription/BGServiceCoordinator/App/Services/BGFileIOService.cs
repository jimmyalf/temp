using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFileIOService : IFileIOService
	{
		public void WriteFile(string filePath, string contents) 
		{
			System.IO.File.WriteAllText(filePath, contents);
		}
		public bool FileExists(string filePath)
		{
			return System.IO.File.Exists(filePath);
		}

	    public string[] ReadFile(string filePath)
	    {
	        return System.IO.File.ReadAllLines(filePath, System.Text.Encoding.GetEncoding(858));
	    }

	    public int GetNumberOfReceivedFiles(string folderPath)
	    {
            var directoryInfo = new DirectoryInfo(folderPath);
            FileInfo[] files = directoryInfo.GetFiles();
	        return files.Length;
	    }

	    public IEnumerable<string> GetReceivedFileNames(string folderPath)
	    {
            var directoryInfo = new DirectoryInfo(folderPath);
            FileInfo[] files = directoryInfo.GetFiles();
	        return files.Select(x => x.Name).ToList();
	    }
	}
}