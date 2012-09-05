using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Extensions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;
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

		public event EventHandler<RenameEventArgs> BaseFileRenamed;
		public event EventHandler<RenameEventArgs> FileRenamed;
		public event EventHandler<RenameEventArgs> DirectoryRenamed;
		public event EventHandler<RenameEventArgs> ContentRenamed;
		public event EventHandler<RenameEventArgs> NewsRenamed;

		public Migrator()
		{
			_renamingService = new RenamingService();
			_fileSystemService = new FileSystemService(Settings.GetCommonFilesPath());
		}

		public IList<FileEntityRenamingResult> RenameDatabaseEntries()
		{
			var output = new List<FileEntityRenamingResult>();
			foreach (var fileEntry in GetInvalidFileEntries())
			{
				var renamedFile = new RenameFileCommand(fileEntry, _renamingService.Rename).Execute();
				if (BaseFileRenamed != null) BaseFileRenamed(this, renamedFile.ToRenameEvent());
				output.Add(renamedFile);
			}
			return output;
		}

		public IList<IRenamingResult> RenameDirectories()
		{
			var output = new List<IRenamingResult>();
			var directoriesToRename = GetInvalidDirectories();
			while (directoriesToRename.Any())
			{
				var directoryToRename = directoriesToRename.First();
				var result = _fileSystemService.Rename(directoryToRename, _renamingService.Rename);
				if (DirectoryRenamed != null) DirectoryRenamed(this, result.ToRenameEvent());
				output.Add(result);
				directoriesToRename = GetInvalidDirectories();
			}
			return output;
		}

		public IList<IRenamingResult> RenameFiles()
		{
			var output = new List<IRenamingResult>();
			foreach (var file in GetInvalidFiles())
			{
				var result = _fileSystemService.Rename(file, _renamingService.Rename);
				if (FileRenamed != null) FileRenamed(this, result.ToRenameEvent());
				output.Add(result);
			}
			return output;
		}

		public IList<ContentUpdateResult> RenameContent(IEnumerable<FileEntityRenamingResult> renamedFiles)
		{
			var output = new List<ContentUpdateResult>();
			foreach (var renamedFile in renamedFiles)
			{
				var matchingPages = new ContentEntitiesMatchingSearchQuery(renamedFile.OldPath).Execute();
				foreach (var matchingPage in matchingPages)
				{
					var result = new RenameContentCommand(matchingPage).Execute(renamedFile.OldPath, renamedFile.NewPath);
					if(ContentRenamed != null) ContentRenamed(this, result.ToRenameEvent());
					output.Add(result);
				}
			}
			return output;
		}

		public IEnumerable<NewsUpdateResult> RenameNews(IEnumerable<FileEntityRenamingResult> renamedFiles)
		{
			var output = new List<NewsUpdateResult>();
			foreach (var renamedFile in renamedFiles)
			{
				var matchingNews = new NewsEntitiesMatchingSearchQuery(renamedFile.OldPath).Execute();
				foreach (var matchingNewsItem in matchingNews)
				{
					var result = new RenameNewsCommand(matchingNewsItem).Execute(renamedFile.OldPath, renamedFile.NewPath);
					if(NewsRenamed != null) NewsRenamed(this, result.ToRenameEvent());
					output.Add(result);
				}
			}
			return output;
		}

		protected virtual IList<FileEntity> GetInvalidFileEntries()
		{
			return new AllFileEntitiesQuery().Execute()
				.Where(_renamingService.FileNeedsRenaming)
				.ToList();
		}

		protected virtual IList<DirectoryInfo> GetInvalidDirectories()
		{
			return _fileSystemService
				.GetAllDirectories()
				.Where(directory => !_renamingService.IsValid(directory.Name)).ToList();
		}

		protected virtual IList<FileInfo> GetInvalidFiles()
		{
			return _fileSystemService
				.GetAllFiles()
				.Where(file => !_renamingService.IsValid(file.Name)).ToList();
		}

		
		protected virtual string GetIllegalChars(IEnumerable<FileEntity> fileCollection)
		{
			return fileCollection
			    .Select(x => x.Name)
			    .Select(_renamingService.ReplaceTokens)
			    .SelectMany(x => x).ConvertToString()
			    .RegexRemove(Settings.ValidCharacterPattern, RegexOptions.IgnoreCase)
			    .ToCharArray().Distinct()
			    .ConvertToString();	
		}
	}
}