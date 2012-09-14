using System;
using System.Xml.Serialization;
using Spinit.Wpc.Maintenance.FileAndContentMigration;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Config;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Extensions;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = Configuration.GetFromConfigFile();
			var migrator = new Migrator(config);
			migrator.BaseFileRenamed += (s, e) => Console.WriteLine(e);
			migrator.DirectoryRenamed += (s, e) => Console.WriteLine(e);
			migrator.FileRenamed += (s, e) => Console.WriteLine(e);
			migrator.ComponentEntityUpdated += (s, e) => Console.WriteLine(e);
			migrator.Initialize();
			migrator.MigrateBaseFiles().Save(Debug.Out);
			var renamedDirectories = migrator.MigrateDirectories();
			var renamedFiles = migrator.MigrateFiles();
			var renamedContent = migrator.MigrateEntity(new ContentMigrator());
			var renamedNews = migrator.MigrateEntity(new NewsMigrator());
			var renamedCourses = migrator.MigrateEntity(new CourseMigrator());
			var renamedMembers = migrator.MigrateEntity(new MemberContentMigrator());
			var renamedOPQDocumentEntries = migrator.MigrateEntity(new OPQDocumentMigrator());
			var renamedOPQDocumentHistoryEntries = migrator.MigrateEntity(new OPQDocumentHistoryMigrator());
			Console.ReadKey();
			var serializer = new XmlSerializer(migrator.GetType());
			serializer.Serialize(Console.Out,migrator);
		}
	}
}
