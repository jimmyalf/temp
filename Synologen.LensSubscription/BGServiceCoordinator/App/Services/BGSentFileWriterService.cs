using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGSentFileWriterService : IFileWriterService
	{
		private readonly IFileIOService _fileIOService;
		private readonly IBGConfigurationSettingsService _bgConfigurationSettingsService;

		public BGSentFileWriterService(IFileIOService fileIOService, IBGConfigurationSettingsService bgConfigurationSettingsService)
		{
			_fileIOService = fileIOService;
			_bgConfigurationSettingsService = bgConfigurationSettingsService;
		}

		public void WriteFileToDisk(string fileContents, string fileName)
		{
			if(fileName.Contains("\\") || fileName.Contains(":"))
			{
				throw new ArgumentException("Filename contains illegal characters", "fileName");
			}
			var sentFilesFolderPath = _bgConfigurationSettingsService.GetSentFilesFolderPath();
			var filePath = System.IO.Path.Combine(sentFilesFolderPath, fileName);
			if(_fileIOService.FileExists(filePath))
			{
				throw new ArgumentException(String.Format("A file ({0}) already exists.", filePath), "fileName");
			}

			_fileIOService.WriteFile(filePath, fileContents);
		}
	}
}