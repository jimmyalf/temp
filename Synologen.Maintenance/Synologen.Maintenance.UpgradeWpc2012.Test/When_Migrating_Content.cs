using System.Linq;
using NUnit.Framework;
using Shouldly;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;
using Synologen.Maintenance.UpgradeWpc2012.Test.Base;
using Synologen.Maintenance.UpgradeWpc2012.Test.Persistence;

namespace Synologen.Maintenance.UpgradeWpc2012.Test
{
	[TestFixture]
	public class When_Migrating_Content : DatabaseTestBase
	{
		[Test]
		public void Using_content_with_ö_and_url_encoded_whitespace()
		{
			//Arrange
			const string fileName = "/commonresources/files/www.synologen.se/torra%20ögon/torraögon.jpg";
			const string expectedRenamedFileName = "/commonresources/files/www.synologen.se/torra_ogon/torraogon.jpg";
			const string content = "<h1><img src=" + fileName + " /></h1>";
			const string expectedRenamedContent = "<h1><img src=" + expectedRenamedFileName + " /></h1>";
			Database.CreateFileEntry(fileName);
			Database.CreateContentEntry(content);

			//Act
			Migrator.RenameDatabaseEntries();
			Migrator.RenameContent();

			//Assert
			var renamedEntry = new AllContentEntitiesQuery().Execute().Single();
			renamedEntry.Content.ShouldBe(expectedRenamedContent);
		}
	}

	[TestFixture]
	public class When_Migrating_News : DatabaseTestBase
	{
		[Test]
		public void Using_news_with_ö_and_url_encoded_whitespace()
		{
			//Arrange
			const string fileName = "/commonresources/files/www.synologen.se/torra%20ögon/torraögon.jpg";
			const string expectedRenamedFileName = "/commonresources/files/www.synologen.se/torra_ogon/torraogon.jpg";
			const string content = "<h1><img src=" + fileName + " /></h1>";
			const string expectedRenamedContent = "<h1><img src=" + expectedRenamedFileName + " /></h1>";
			Database.CreateFileEntry(fileName);
			Database.CreateNewsEntry(content, content);

			//Act
			Migrator.RenameDatabaseEntries();
			Migrator.RenameNews();

			//Assert
			var renamedEntry = new AllNewsEntitiesQuery().Execute().Single();
			renamedEntry.Body.ShouldBe(expectedRenamedContent);
			renamedEntry.FormatedBody.ShouldBe(expectedRenamedContent);
		}		
	}
}