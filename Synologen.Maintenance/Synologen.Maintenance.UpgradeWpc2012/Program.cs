using System;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	class Program
	{
		static void Main(string[] args)
		{
			var migrator = new Migrator();
			migrator.AllRenameEvents += (s, e) => Console.WriteLine(e);

			var renamedDatabaseEntries = migrator.RenameDatabaseEntries();
			var renamedDirectories = migrator.RenameDirectories();
			var renamedFiles = migrator.RenameFiles();
			var renamedContent = migrator.RenameContent();
			var renamedNews = migrator.RenameNews();
			var renamedCourses = migrator.RenameCourses();
			var renamedMemberContent = migrator.RenameMemberContents();
			Console.ReadKey();
		}
	}
}
