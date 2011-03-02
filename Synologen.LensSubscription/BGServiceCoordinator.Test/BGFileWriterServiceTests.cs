using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Synologen.LensSubscription.BGService.Test.TestHelpers;

namespace Synologen.LensSubscription.BGService.Test
{
	[TestFixture, Category("BGSentFileWriterServiceTests")]
	public class When_writing_file_to_disk : BGSentFileWriterServiceTestBase
	{
		private string fileData;
		private string ftpFileName;
		private string filesFolderPath;
		private string filePath;
		private string expectedCreatedFileName;

		public When_writing_file_to_disk()
		{
			Context = () =>
			{
				WriteDate = new DateTime(2011, 03, 02, 13, 52, 15);
				fileData = "ABCDEFG";
				ftpFileName = "BFEP.IGAG.K123456";
				expectedCreatedFileName = string.Format("{0}.D110302.T135215", ftpFileName);
				filesFolderPath = @"C:\Folder1\Folder2\Folder3";
				filePath = String.Format(@"{0}\{1}", filesFolderPath, expectedCreatedFileName);
				A.CallTo(() => BGConfigurationSettingsService.GetSentFilesFolderPath()).Returns(filesFolderPath);
			};
			Because = fileWriterService => fileWriterService.WriteFileToDisk(fileData, ftpFileName);
		}

		[Test]
		public void File_path_is_fetched_from_settings_service()
		{
			A.CallTo(() => BGConfigurationSettingsService.GetSentFilesFolderPath()).MustHaveHappened();
		}

		[Test]
		public void File_is_written_to_disk_using_io_service()
		{
			A.CallTo(() => FileIOService.WriteFile(filePath, fileData)).MustHaveHappened();
		}
	}

	[TestFixture, Category("BGSentFileWriterServiceTests")]
	public class When_writing_file_to_disk_and_a_file_already_exists : BGSentFileWriterServiceTestBase
	{
		private string fileData;
		private string fileName;
		private string filesFolderPath;
		private Exception caughtException;

		public When_writing_file_to_disk_and_a_file_already_exists()
		{
			Context = () =>
			{
				fileData = "ABCDEFG";
				fileName = "testfile.txt";
				filesFolderPath = @"C:\Folder1\Folder2\Folder3";
				A.CallTo(() => BGConfigurationSettingsService.GetSentFilesFolderPath()).Returns(filesFolderPath);
				A.CallTo(() => FileIOService.FileExists(A<string>.Ignored)).Returns(true);
			};
			Because = fileWriterService =>
			{
				try { fileWriterService.WriteFileToDisk(fileData, fileName); }
				catch(Exception ex) { caughtException = ex; }

			};
		}

		[Test]
		public void An_illegal_argument_exception_is_thrown()
		{
			caughtException.ShouldBeTypeOf(typeof(ArgumentException));
		}

	}

	[TestFixture, Category("BGSentFileWriterServiceTests")]
	public class When_writing_file_to_disk_with_illegal_file_name : BGSentFileWriterServiceTestBase
	{
		private string fileData;
		private string fileName;
		private string filesFolderPath;
		private Exception caughtException;

		public When_writing_file_to_disk_with_illegal_file_name()
		{
			Context = () =>
			{
				fileData = "ABCDEFG";
				fileName = @"\\Folder\testfile.txt";
				filesFolderPath = @"C:\Folder1\Folder2\Folder3";
				A.CallTo(() => BGConfigurationSettingsService.GetSentFilesFolderPath()).Returns(filesFolderPath);
			};
			Because = fileWriterService =>
			{
				try { fileWriterService.WriteFileToDisk(fileData, fileName); }
				catch(Exception ex) { caughtException = ex; }

			};
		}

		[Test]
		public void An_illegal_argument_exception_is_thrown()
		{
			caughtException.ShouldBeTypeOf(typeof(ArgumentException));
		}
	}

}