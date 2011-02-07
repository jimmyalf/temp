using System.IO;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class AutogiroFileReader<TFile>
	{
		private readonly IAutogiroFileReader<TFile> _fileReader;
		private readonly string _filePath;

		public AutogiroFileReader(IAutogiroFileReader<TFile> fileReader, string filePath)
		{
			_fileReader = fileReader;
			_filePath = filePath;
		}

		public TFile Read()
		{
			if(!FileExists) throw new AutogiroFileDoesNotExistException("No file could be found on path \"{0}\"", _filePath, _filePath);
			var fileContent = File.ReadAllText(_filePath);
			return _fileReader.Read(fileContent);
		}

		public bool FileExists
		{
			get { return File.Exists(_filePath); }
		}
	}
}