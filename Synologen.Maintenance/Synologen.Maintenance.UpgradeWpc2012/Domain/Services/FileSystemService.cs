using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Services
{
	public class FileSystemService
	{
		private readonly string _rootPath;

		public FileSystemService(string rootPath)
		{
			_rootPath = rootPath;
		}

		public IEnumerable<DirectoryInfo> GetAllDirectories()
		{
			return Directory.GetDirectories(_rootPath, "*", SearchOption.AllDirectories).Select(path => new DirectoryInfo(path));
		}

		public IEnumerable<FileInfo> GetAllFiles()
		{
			return Directory.GetFiles(_rootPath, "*", SearchOption.AllDirectories).Select(path => new FileInfo(path));
		}

		public IRenamingResult Rename(DirectoryInfo directoryInfo, Func<string,string> renameFile)
		{
			var oldPath = directoryInfo.FullName;
			var parentPath = Directory.GetParent(oldPath);
			var newName = renameFile(directoryInfo.Name);
			var newPath = Path.Combine(parentPath.FullName, newName);
			Directory.Move(oldPath, newPath);
			return new DirectoryRenamingResult(oldPath, newPath);
		}

		public IRenamingResult Rename(FileInfo fileInfo, Func<string,string> renameFile)
		{
			var newName = renameFile(fileInfo.Name);
			var newPath = Path.Combine(fileInfo.Directory.FullName, newName);
			fileInfo.MoveTo(newPath);
			return new FileRenamingResult(fileInfo.FullName, newPath);
		}
	}


}