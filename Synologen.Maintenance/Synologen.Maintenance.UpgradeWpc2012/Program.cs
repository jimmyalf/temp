using System;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	class Program
	{
		static void Main(string[] args)
		{
			var migrator = new Migrator();
			migrator.AllRenameEvents += (s, e) => Console.WriteLine(e);

			var renamedDatabaseEntries = migrator.RenameBaseFilesEntries();
			var renamedDirectories = migrator.RenameDirectories();
			var renamedFiles = migrator.RenameFiles();
			var renamedContent = migrator.MigrateComponent(new ContentMigrator());
			var renamedNews = migrator.MigrateComponent(new NewsMigrator());
			var renamedCourses = migrator.MigrateComponent(new CourseMigrator());
			var renamedMembers = migrator.MigrateComponent(new MemberMigrator());
			Console.ReadKey();
		}
	}
}
