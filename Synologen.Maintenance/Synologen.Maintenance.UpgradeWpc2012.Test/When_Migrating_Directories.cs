using System.Linq;
using NUnit.Framework;
using Shouldly;
using Synologen.Maintenance.UpgradeWpc2012.Test.Base;

namespace Synologen.Maintenance.UpgradeWpc2012.Test
{
	[TestFixture]
	public class When_Migrating_Directories : FileSystemTestBase
	{
		[Test]
		public void Using_directory_with_block_parenthesis()
		{
			//Arrange
			CreateDirectory("Test[with][dir]");

			//Act
			Migrator.RenameDirectories();

			//Assert
			GetAllDirectories().Single().Name.ShouldBe("Testwithdir");
		}

		[Test]
		public void Using_directory_with_parentheses()
		{
			//Arrange
			CreateDirectory("Test(with)(dir)");

			//Act
			Migrator.RenameDirectories();

			//Assert
			GetAllDirectories().Single().Name.ShouldBe("Testwithdir");
		}

		[Test]
		public void Using_directory_with_åäö()
		{
			//Arrange
			CreateDirectory("TestWithåäö");

			//Act
			Migrator.RenameDirectories();

			//Assert
			GetAllDirectories().Single().Name.ShouldBe("TestWithaao");
		}

		[Test]
		public void Using_directory_with_ÅÄÖ()
		{
			//Arrange
			CreateDirectory("TestWithÅÄÖ");

			//Act
			Migrator.RenameDirectories();

			//Assert
			GetAllDirectories().Single().Name.ShouldBe("TestWithAAO");
		}

		[Test]
		public void Using_directory_with_whitespace()
		{
			//Arrange
			CreateDirectory("Test dir");

			//Act
			Migrator.RenameDirectories();

			//Assert
			GetAllDirectories().Single().Name.ShouldBe("Test_dir");
		}

		[Test]
		public void Using_subdirectory()
		{
			//Arrange
			CreateDirectory("Test[dir]");
			CreateDirectory("Test[dir]/Another[dir]");

			//Act
			Migrator.RenameDirectories();

			//Assert
			GetDirectory("Testdir").Exists.ShouldBe(true);
			GetDirectory("Testdir/Anotherdir").Exists.ShouldBe(true);
		}
	}
}