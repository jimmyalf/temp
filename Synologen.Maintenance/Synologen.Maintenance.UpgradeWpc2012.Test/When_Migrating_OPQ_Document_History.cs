using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators;
using Synologen.Maintenance.UpgradeWpc2012.Test.Base;
using Synologen.Maintenance.UpgradeWpc2012.Test.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Test
{
	[TestFixture]
	public class When_Migrating_OPQ_Document_History : DatabaseTestBase
	{
		[Test]
		public void Using_course_with_ö_and_url_encoded_whitespace()
		{
			//Arrange
			const string fileName = "/commonresources/files/www.synologen.se/torra%20ögon/torraögon.jpg";
			const string expectedRenamedFileName = "/commonresources/files/www.synologen.se/torra-ogon/torraogon.jpg";
			const string content = "<h1><img src=" + fileName + " /></h1>";
			const string expectedRenamedContent = "<h1><img src=" + expectedRenamedFileName + " /></h1>";
			Database.CreateFileEntry(fileName);
			Database.CreateOPQDocumentHistoryEntry(content);

			//Act
			Migrator.MigrateBaseFiles().Save(Console.Out);
			Migrator.MigrateEntity(new OPQDocumentHistoryMigrator()).Save(Console.Out);;

			//Assert
			var renamedEntry = Query(new AllOPQDocumentHistoryEntitiesQuery()).Single();
			renamedEntry.DocumentContent.ShouldBe(expectedRenamedContent);
		}

		[Test]
		public void Using_content_with_ö_and_url_encoded_content_paths()
		{
			const string fileName = "/commonresources/files/www.synologen.se/torra ögon/torra ögon.jpg";
			const string content = "<h1><img src=\"/commonresources/files/www.synologen.se/torra%20%C3%B6gon/torra%20%C3%B6gon.jpg\" /></h1>";
			const string expectedContent = "<h1><img src=\"/commonresources/files/www.synologen.se/torra-ogon/torra-ogon.jpg\" /></h1>";
			//Arrange
			Database.CreateFileEntry(fileName);
			Database.CreateOPQDocumentHistoryEntry(content);

			//Act
			Migrator.MigrateBaseFiles();
			Migrator.MigrateEntity(new OPQDocumentHistoryMigrator());

			//Assert
			var renamedEntry = Query(new AllOPQDocumentHistoryEntitiesQuery()).Single();
			renamedEntry.DocumentContent.ShouldBe(expectedContent);
		}
	}
}