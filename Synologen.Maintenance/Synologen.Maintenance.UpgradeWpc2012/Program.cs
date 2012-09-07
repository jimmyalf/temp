using System;
using Spinit.Wpc.Maintenance.FileAndContentMigration;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	class Program
	{
		static void Main(string[] args)
		{
			var migrator = new Migrator();
			migrator.BaseFileRenamed += (s, e) => Console.WriteLine(e);
			migrator.DirectoryRenamed += (s, e) => Console.WriteLine(e);
			migrator.FileRenamed += (s, e) => Console.WriteLine(e);
			migrator.ComponentEntityUpdated += (s, e) => Console.WriteLine(e);

			var renamedDatabaseEntries = migrator.RenameBaseFilesEntries();
			var renamedDirectories = migrator.RenameDirectories();
			var renamedFiles = migrator.RenameFiles();
			var renamedContent = migrator.MigrateEntity(new ContentMigrator());
			var renamedNews = migrator.MigrateEntity(new NewsMigrator());
			var renamedCourses = migrator.MigrateEntity(new CourseMigrator());
			var renamedMembers = migrator.MigrateEntity(new MemberMigrator());
			var renamedOPQDocumentEntries = migrator.MigrateEntity(new OPQDocumentMigrator());
			var renamedOPQDocumentHistoryEntries = migrator.MigrateEntity(new OPQDocumentHistoryMigrator());
			Console.ReadKey();
		}
	}
}
