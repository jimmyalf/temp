using System.Linq;
using NUnit.Framework;
using Shouldly;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;
using Synologen.Maintenance.UpgradeWpc2012.Test.Persistence;

namespace Synologen.Maintenance.UpgradeWpc2012.Test
{
	public class When_Migrating_Base_Files : DatabaseTestBase
	{
		[Test]
		public void Using_file_with_block_parentheses()
		{
			//Arrange
			Database.CreateFileEntry("/Test/file[name].pdf");

			//Act
			var result = Migrator.RenameDatabaseEntries();

			//Assert
			var renamedFileEntry = new AllFileEntitiesQuery().Execute().Single();
			renamedFileEntry.Name.ShouldBe("/Test/filename.pdf");
		}

		[Test]
		public void Using_file_with_parentheses()
		{
			//Arrange
			Database.CreateFileEntry("/Test/file(name).pdf");

			//Act
			var result = Migrator.RenameDatabaseEntries();

			//Assert
			var renamedFileEntry = new AllFileEntitiesQuery().Execute().Single();
			renamedFileEntry.Name.ShouldBe("/Test/filename.pdf");
		}

		[Test]
		public void Using_file_with_åäö()
		{
			//Arrange
			Database.CreateFileEntry("/Test/fileåäö.pdf");

			//Act
			var result = Migrator.RenameDatabaseEntries();

			//Assert
			var renamedFileEntry = new AllFileEntitiesQuery().Execute().Single();
			renamedFileEntry.Name.ShouldBe("/Test/fileaao.pdf");
		}

		[Test]
		public void Using_file_with_ÅÄÖ()
		{
			//Arrange
			Database.CreateFileEntry("/Test/fileÅÄÖ.pdf");

			//Act
			var result = Migrator.RenameDatabaseEntries();

			//Assert
			var renamedFileEntry = new AllFileEntitiesQuery().Execute().Single();
			renamedFileEntry.Name.ShouldBe("/Test/fileAAO.pdf");
		}

		[Test]
		public void Using_file_with_whitespace()
		{
			//Arrange
			Database.CreateFileEntry("/Test/file name.pdf");

			//Act
			var result = Migrator.RenameDatabaseEntries();

			//Assert
			var renamedFileEntry = new AllFileEntitiesQuery().Execute().Single();
			renamedFileEntry.Name.ShouldBe("/Test/file_name.pdf");
		}
	}
}
