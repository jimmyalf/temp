using System.IO;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class AutogiroFileWriter<TFile>
	{
		private readonly IAutogiroFileWriter<TFile> _fileWriter;
		private readonly string _filePath;


		public AutogiroFileWriter(IAutogiroFileWriter<TFile> fileWriter, string filePath)
		{
			_fileWriter = fileWriter;
			_filePath = filePath;
		}

		public void Write(TFile file)
		{
			if (FileExists) throw new AutogiroFileExistsException("File already exists on path \"{0}\"", _filePath);
			var content = _fileWriter.Write(file);
			File.WriteAllText(_filePath, content);
		}

		public bool FileExists
		{
			get { return File.Exists(_filePath); }
		}
	}
}