using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Extensions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Services;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Settings;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	public class Migrator
	{
		private readonly RenamingService _renamingService;
		private readonly FileSystemService _fileSystemService;

		public Migrator()
		{
			_renamingService = new RenamingService();
			_fileSystemService = new FileSystemService(@"C:\Develop\WPC\Synologen\Common\Files\");
		}
		public void RenameDatabaseEntries()
		{
			var fileCollection = new BaseFileCollectionQuery().Execute()
				.Where(_renamingService.FileNeedsRenaming)
				.ToList();
			foreach (var fileEntry in fileCollection)
			{
				var renamedFile = new RenameFileCommand(fileEntry, _renamingService.Rename).Execute();
			}
		}

		public void RenameDirectories()
		{
			var directoriesToRename = GetInvalidDirectories();
			while (directoriesToRename.Any())
			{
				var directoryToRename = directoriesToRename.First();
				var result = _fileSystemService.Rename(directoryToRename, _renamingService.Rename);
				Debug.WriteLine("Moved directory from {0} to {1}", result.OldPath, result.NewPath);
				directoriesToRename = GetInvalidDirectories();
			}			
		}

		public void RenameFiles()
		{
			foreach (var file in GetInvalidFiles())
			{
				var result = _fileSystemService.Rename(file, _renamingService.Rename);
				Debug.WriteLine("Renamed file from {0} to {1}", result.OldPath, result.NewPath);		
			}
		}

		protected string GetIllegalChars(FileCollection fileCollection)
		{
			return fileCollection
			    .Select(x => x.Name)
			    .Select(_renamingService.ReplaceTokens)
			    .SelectMany(x => x).ConvertToString()
			    .RegexRemove(Settings.ValidCharacterPattern, RegexOptions.IgnoreCase)
			    .ToCharArray().Distinct()
			    .ConvertToString();			
		}

		protected IList<DirectoryInfo> GetInvalidDirectories()
		{
			return _fileSystemService
				.GetAllDirectories()
				.Where(directory => !_renamingService.IsValid(directory.Name)).ToList();
		}

		protected IList<FileInfo> GetInvalidFiles()
		{
			return _fileSystemService
				.GetAllFiles()
				.Where(directory => !_renamingService.IsValid(directory.Name)).ToList();
		}
	}
}