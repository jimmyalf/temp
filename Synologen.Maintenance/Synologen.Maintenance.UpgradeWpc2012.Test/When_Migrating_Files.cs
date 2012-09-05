using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Synologen.Maintenance.UpgradeWpc2012.Test
{
	[TestFixture]
	public class When_Migrating_Files : FileSystemTestBase
	{
		[Test]
		public void Using_file_with_block_parenthesis()
		{
			//Arrange
			CreateFile("Test[file].pdf");

			//Act
			Migrator.RenameFiles();

			//Assert
			GetAllFiles().Single().Name.ShouldBe("Testfile.pdf");
		}

		[Test]
		public void Using_file_with_parentheses()
		{
			//Arrange
			CreateFile("Test(file).pdf");

			//Act
			Migrator.RenameFiles();

			//Assert
			GetAllFiles().Single().Name.ShouldBe("Testfile.pdf");
		}

		[Test]
		public void Using_file_with_åäö()
		{
			//Arrange
			CreateFile("Fileåäö.pdf");

			//Act
			Migrator.RenameFiles();

			//Assert
			GetAllFiles().Single().Name.ShouldBe("Fileaao.pdf");
		}

		[Test]
		public void Using_file_with_ÅÄÖ()
		{
			//Arrange
			CreateFile("FileÅÄÖ.pdf");

			//Act
			Migrator.RenameFiles();

			//Assert
			GetAllFiles().Single().Name.ShouldBe("FileAAO.pdf");
		}

		[Test]
		public void Using_file_with_whitespace()
		{
			//Arrange
			CreateFile("File name.pdf");

			//Act
			Migrator.RenameFiles();

			//Assert
			GetAllFiles().Single().Name.ShouldBe("File_name.pdf");
		}

		[Test]
		public void Using_file_in_subdirectory()
		{
			//Arrange
			CreateDirectory("TestDir");
			CreateFile("TestDir/Test[file].pdf");

			//Act
			Migrator.RenameFiles();

			//Assert
			GetAllFiles("*.*", SearchOption.AllDirectories).Single().Name.ShouldBe("Testfile.pdf");
		}
	}
}