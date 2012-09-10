using System.Linq;
using NUnit.Framework;
using Shouldly;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators;
using Synologen.Maintenance.UpgradeWpc2012.Test.Base;
using Synologen.Maintenance.UpgradeWpc2012.Test.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Test
{
	[TestFixture]
	public class When_Migrating_OPQ_Document : DatabaseTestBase
	{
		[Test]
		public void Using_course_with_ö_and_url_encoded_whitespace()
		{
			//Arrange
			const string fileName = "/commonresources/files/www.synologen.se/torra%20ögon/torraögon.jpg";
			const string expectedRenamedFileName = "/commonresources/files/www.synologen.se/torra_ogon/torraogon.jpg";
			const string content = "<h1><img src=" + fileName + " /></h1>";
			const string expectedRenamedContent = "<h1><img src=" + expectedRenamedFileName + " /></h1>";
			Database.CreateFileEntry(fileName);
			Database.CreateOPQDocumentEntry(content);

			//Act
			Migrator.MigrateBaseFiles();
			Migrator.MigrateEntity(new OPQDocumentMigrator());

			//Assert
			var renamedEntry = new AllOPQDocumentEntitiesQuery().Execute().Single();
			renamedEntry.DocumentContent.ShouldBe(expectedRenamedContent);
		}		
	}

	[TestFixture]
	public class When_Migrating_OPQ_Document_History : DatabaseTestBase
	{
		[Test]
		public void Using_course_with_ö_and_url_encoded_whitespace()
		{
			//Arrange
			const string fileName = "/commonresources/files/www.synologen.se/torra%20ögon/torraögon.jpg";
			const string expectedRenamedFileName = "/commonresources/files/www.synologen.se/torra_ogon/torraogon.jpg";
			const string content = "<h1><img src=" + fileName + " /></h1>";
			const string expectedRenamedContent = "<h1><img src=" + expectedRenamedFileName + " /></h1>";
			Database.CreateFileEntry(fileName);
			Database.CreateOPQDocumentHistoryEntry(content);

			//Act
			Migrator.MigrateBaseFiles();
			Migrator.MigrateEntity(new OPQDocumentHistoryMigrator());

			//Assert
			var renamedEntry = new AllOPQDocumentHistoryEntitiesQuery().Execute().Single();
			renamedEntry.DocumentContent.ShouldBe(expectedRenamedContent);
		}		
	}
}