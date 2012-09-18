using System;
using System.IO;
using System.Xml.Serialization;
using Spinit.Wpc.Maintenance.FileAndContentMigration;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Config;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Extensions;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
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
			migrator.MigrateBaseFiles().SaveTo("BaseFiles.xml");
			migrator.MigrateDirectories().SaveTo("Directories.xml");
			migrator.MigrateFiles().SaveTo("Files.xml");
			migrator.MigrateEntity(new ContentMigrator()).SaveTo("Content.xml");
			migrator.MigrateEntity(new NewsMigrator()).SaveTo("News.xml");
			migrator.MigrateEntity(new CourseMigrator()).SaveTo("Course.xml");
			migrator.MigrateEntity(new MemberContentMigrator()).SaveTo("MemberContent.xml");
			migrator.MigrateEntity(new OPQDocumentMigrator()).SaveTo("OPQDocument.xml");
			migrator.MigrateEntity(new OPQDocumentHistoryMigrator()).SaveTo("OPQDocumentHistory.xml");
			Console.ReadKey();
		}
	}

	public static class ResultExtensions
	{
		public static void SaveTo<TEntity>(this IOperationResultCollection<TEntity> resultCollection,string fileName)
		{
			var path = Path.Combine(@"C:\Users\cber\Desktop\MigrationResults\", fileName);
			resultCollection.Save(path);
		}
	}
}
