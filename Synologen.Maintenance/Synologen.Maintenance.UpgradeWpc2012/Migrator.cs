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
using log4net;
using log4net.Config;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	public class Migrator
	{
		private readonly RenamingService _renamingService;
		private readonly FileSystemService _fileSystemService;
		private readonly ILog _logger;

		public event EventHandler<RenameEventArgs> BaseFileRenamed;
		public event EventHandler<RenameEventArgs> FileRenamed;
		public event EventHandler<RenameEventArgs> DirectoryRenamed;
		public event EventHandler<RenameEventArgs> ContentRenamed;
		public event EventHandler<RenameEventArgs> NewsRenamed;
		public event EventHandler<RenameEventArgs> AllRenameEvents;

		public Migrator()
		{
			_renamingService = new RenamingService();
			_fileSystemService = new FileSystemService(Settings.CommonFilesDirectory);
			_logger = LogManager.GetLogger(GetType());
			XmlConfigurator.Configure();
		}

		public IList<FileEntityRenamingResult> RenameDatabaseEntries()
		{
			var output = new List<FileEntityRenamingResult>();
			foreach (var fileEntry in GetInvalidFileEntries())
			{
				try
				{
					var renamedFile = new RenameFileCommand(fileEntry, _renamingService.Rename).Execute();
					FireEventAndLog(BaseFileRenamed, renamedFile.ToRenameEvent());
					output.Add(renamedFile);
				}
				catch (Exception ex)
				{
					_logger.Error("Got exception while renaming BaseFile[" + fileEntry.Id + "]", ex);
				}
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
				try
				{
					var result = _fileSystemService.Rename(directoryToRename, _renamingService.Rename);
					FireEventAndLog(DirectoryRenamed, result.ToRenameEvent());
					output.Add(result);
					directoriesToRename = GetInvalidDirectories();
				}
				catch (Exception ex)
				{
					_logger.Error("Got exception while renaming Directory[" + directoryToRename.FullName + "]", ex);
				}
			}
			return output;
		}

		public IList<IRenamingResult> RenameFiles()
		{
			var output = new List<IRenamingResult>();
			foreach (var file in GetInvalidFiles())
			{
				try
				{
					var result = _fileSystemService.Rename(file, _renamingService.Rename);
					FireEventAndLog(FileRenamed, result.ToRenameEvent());
					output.Add(result);
				}
				catch (Exception ex)
				{
					_logger.Error("Got exception while renaming File[" + file.FullName + "]", ex);
				}
			}
			return output;
		}

		public IList<ContentUpdateResult> RenameContent()
		{
			var output = new List<ContentUpdateResult>();
			var renamedFiles = new AllRenamedFileEntitiesQuery().Execute();
			foreach (var renamedFile in renamedFiles)
			{
				var matchingPages = new ContentEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
				foreach (var matchingPage in matchingPages)
				{
					try
					{
						var result = new RenameContentCommand(matchingPage).Execute(renamedFile.PreviousName, renamedFile.Name);
						FireEventAndLog(ContentRenamed, result.ToRenameEvent());
						output.Add(result);
					}
					catch (Exception ex)
					{
						_logger.Error("Got exception while renaming ContentPage[" + matchingPage.Id + "]", ex);
					}
				}
			}
			return output;
		}

		public IEnumerable<NewsUpdateResult> RenameNews()
		{
			var output = new List<NewsUpdateResult>();
			var renamedFiles = new AllRenamedFileEntitiesQuery().Execute();
			foreach (var renamedFile in renamedFiles)
			{
				var matchingNews = new NewsEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
				foreach (var matchingNewsItem in matchingNews)
				{
					try
					{
						var result = new RenameNewsCommand(matchingNewsItem).Execute(renamedFile.PreviousName, renamedFile.Name);
						FireEventAndLog(NewsRenamed, result.ToRenameEvent());
						output.Add(result);
					}
					catch (Exception ex)
					{
						_logger.Error("Got exception while renaming News[" + matchingNewsItem.Id + "]", ex);
					}
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

		private void FireEventAndLog(EventHandler<RenameEventArgs> eventHandler, RenameEventArgs eventArgs)
		{
			if (eventHandler != null) eventHandler(this, eventArgs);
			if (AllRenameEvents != null) AllRenameEvents(this, eventArgs);
			_logger.Info(eventArgs.Description);
		}
	}
}