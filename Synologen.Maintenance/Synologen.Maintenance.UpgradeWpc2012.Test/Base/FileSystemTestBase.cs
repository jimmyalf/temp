using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Settings;
using Synologen.Maintenance.UpgradeWpc2012.Test.Persistence;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Base
{
	public class FileSystemTestBase
	{
		protected Migrator Migrator;

		[SetUp]
		protected void Setup()
		{
			FileSystem.CleanCommonFilesFolder();
			Migrator = new Migrator();
			Migrator.Initialize();
		}

		protected string CreateFile(string relativePath)
		{
			var root = Settings.CommonFilesDirectory;
			var path = Path.Combine(root.FullName, relativePath);
			File.AppendAllText(path, "Testfile", Encoding.UTF8);
			return path;
		}

		protected string CreateDirectory(string relativePath)
		{
			var root = Settings.CommonFilesDirectory;
			var path = Path.Combine(root.FullName, relativePath);
			Directory.CreateDirectory(path);
			return path;
		}

		protected IEnumerable<FileInfo> GetAllFiles(string searchPattern = null, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return searchPattern != null 
				? Settings.CommonFilesDirectory.GetFiles(searchPattern, searchOption) 
				: Settings.CommonFilesDirectory.GetFiles();
		}

		protected IEnumerable<DirectoryInfo> GetAllDirectories(string searchPattern = null, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			return searchPattern != null 
				? Settings.CommonFilesDirectory.GetDirectories(searchPattern, searchOption) 
				: Settings.CommonFilesDirectory.GetDirectories();
		}

		protected DirectoryInfo GetDirectory(string relativePath)
		{
			var root = Settings.CommonFilesDirectory;
			var path = Path.Combine(root.FullName, relativePath);
			return new DirectoryInfo(path);
		}

		[TearDown]
		protected void TearDown()
		{
			
		}	
	}
}