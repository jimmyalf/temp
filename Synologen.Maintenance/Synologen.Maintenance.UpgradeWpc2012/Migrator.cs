using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Extensions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators;
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
		public event EventHandler<RenameEventArgs> AllRenameEvents;
		public event EventHandler<RenameEventArgs> ComponentEntityUpdated;
		private bool _initialized;

		public Migrator()
		{
			_renamingService = new RenamingService();
			_fileSystemService = new FileSystemService(Settings.CommonFilesDirectory);
			_logger = LogManager.GetLogger(GetType());
			XmlConfigurator.Configure();
		}

		public void Initialize()
		{
			new AddPreviousNameColumnToBaseFileTableCommand().Execute();
			_initialized = true;
		}

		public virtual IList<FileEntityRenamingResult> RenameBaseFilesEntries()
		{
			ValidateInitialized();
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

		public virtual IList<IRenamingResult> RenameDirectories()
		{
			ValidateInitialized();
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

		public virtual IList<IRenamingResult> RenameFiles()
		{
			ValidateInitialized();
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

		public virtual IList<IEntityMigratedResult> MigrateComponent<TEntity>(IComponentMigrator<TEntity> migrator) where TEntity : IEntity
		{
			ValidateInitialized();
			var output = new List<IEntityMigratedResult>();
			var renamedFiles = new AllRenamedFileEntitiesQuery().Execute();
			foreach (var renamedFile in renamedFiles)
			{
				var matchingEntities = migrator.GetEntitiesToBeMigrated(renamedFile);
				foreach (var matchingEntity in matchingEntities)
				{
				    try
				    {
				        var result = migrator.MigrateEntity(renamedFile, matchingEntity);
				        FireEventAndLog(ComponentEntityUpdated, result.ToRenameEvent());
				        output.Add(result);
				    }
				    catch (Exception ex)
				    {
				        _logger.Error("Got exception while updating "+migrator.ComponentName+"[" + matchingEntity.Id + "]", ex);
				    }
				}
			}
			return output;			
		}

		public virtual string GetIllegalChars(IEnumerable<FileEntity> fileCollection)
		{
			return fileCollection
			    .Select(x => x.Name)
			    .Select(_renamingService.ReplaceTokens)
			    .SelectMany(x => x).ConvertToString()
			    .RegexRemove(Settings.ValidCharacterPattern, RegexOptions.IgnoreCase)
			    .ToCharArray().Distinct()
			    .ConvertToString();	
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

		protected virtual void FireEventAndLog(EventHandler<RenameEventArgs> eventHandler, RenameEventArgs eventArgs)
		{
			if (eventHandler != null) eventHandler(this, eventArgs);
			if (AllRenameEvents != null) AllRenameEvents(this, eventArgs);
			_logger.Info(eventArgs.Description);
		}

		[DebuggerStepThrough]
		protected virtual void ValidateInitialized()
		{
			if(!_initialized)
			{
				throw new ApplicationException("Migrator has not been initialized. Make sure the initalize method is run before migration starts.");
			}			
		}
	}
}