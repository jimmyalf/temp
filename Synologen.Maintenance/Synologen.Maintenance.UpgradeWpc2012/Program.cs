using System;
using System.Diagnostics;
using System.Linq;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	class Program
	{
		static void Main(string[] args)
		{
			var migrator = new Migrator();
			migrator.BaseFileRenamed += (s, e) => LogRename(e);
			migrator.DirectoryRenamed += (s, e) => LogRename(e);
			migrator.FileRenamed += (s, e) => LogRename(e);
			migrator.ContentRenamed += (s, e) => LogRename(e);
			migrator.NewsRenamed += (s, e) => LogRename(e);

			var renamedDatabaseEntries = migrator.RenameDatabaseEntries().ToList();
			var renamedDirectories = migrator.RenameDirectories();
			var renamedFiles = migrator.RenameFiles();
			var renamedContent = migrator.RenameContent(renamedDatabaseEntries);
			var renamedNews = migrator.RenameNews(renamedDatabaseEntries);
			Console.ReadKey();
		}

		public static void LogRename(RenameEventArgs e)
		{
			Console.WriteLine(e.Description);
			Debug.WriteLine(e.Description);
		}
	}
}
