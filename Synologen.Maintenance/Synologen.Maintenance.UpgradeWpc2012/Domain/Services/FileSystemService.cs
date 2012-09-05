using System;
using System.Collections.Generic;
using System.IO;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Services
{
	public class FileSystemService
	{
		private readonly DirectoryInfo _root;

		public FileSystemService(DirectoryInfo root)
		{
			_root = root;
		}

		public IEnumerable<DirectoryInfo> GetAllDirectories()
		{
			return _root.GetDirectories("*", SearchOption.AllDirectories);
		}

		public IEnumerable<FileInfo> GetAllFiles()
		{
			return _root.GetFiles("*", SearchOption.AllDirectories);
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