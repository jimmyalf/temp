using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Shouldly;
using NUnit.Framework;
using Synologen.LensSubscription.BGService.Test.Factories;
using Synologen.LensSubscription.BGService.Test.TestHelpers;

namespace Synologen.LensSubscription.BGService.Test
{
    [TestFixture, Category("BGReceivedFileReaderServiceTests")]
    public class When_GetFileNames_is_called_and_no_files_are_found : BGReceivedFileServiceTestBase
    {
        private string _downloadFolderPath = @"c:\download";
        private IEnumerable<string> _returnedFileNames;

        public When_GetFileNames_is_called_and_no_files_are_found()
        {
            Context = () =>
			{
                A.CallTo(() => BGConfigurationSettingsService.GetReceivedFilesFolderPath()).Returns(_downloadFolderPath);
                A.CallTo(() => FileIOService.GetReceivedFileNames(A<string>.Ignored)).Returns(Enumerable.Empty<string>());
			};
			Because = receivedFileReaderService =>
            {
              _returnedFileNames = receivedFileReaderService.GetFileNames();
            };
		}

        [Test]
        public void Service_calls_fileioservice_for_list_of_filenames()
        {
            A.CallTo(() => FileIOService.GetReceivedFileNames(_downloadFolderPath)).MustHaveHappened();
        }

        [Test]
        public void Service_returns_empty_list()
        {
            _returnedFileNames.Count().ShouldBe(0);
        }
    }

    [TestFixture, Category("BGReceivedFileReaderServiceTests")]
    public class When_GetFileNames_is_called_and_only_files_with_invalid_filenames_are_found : BGReceivedFileServiceTestBase
    {
        private string _downloadFolderPath = @"c:\download";
        private IEnumerable<string> _fileNames;
        private IEnumerable<string> _returnedFileNames;

        public When_GetFileNames_is_called_and_only_files_with_invalid_filenames_are_found()
        {
            Context = () =>
            {
                _fileNames = BGReceivedFileReaderServiceFactory.GetInvalidFileNames();
                A.CallTo(() => FileSplitter.FileNameOk(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(false);
                A.CallTo(() => BGConfigurationSettingsService.GetReceivedFilesFolderPath()).Returns(_downloadFolderPath);
                A.CallTo(() => FileIOService.GetReceivedFileNames(A<string>.Ignored)).Returns(_fileNames);
            };
            Because = receivedFileReaderService =>
            {
                _returnedFileNames = receivedFileReaderService.GetFileNames();
            };
        }

        [Test]
        public void Service_returns_empty_list()
        {
            _returnedFileNames.Count().ShouldBe(0);
        }
    }

    [TestFixture, Category("BGReceivedFileReaderServiceTests")]
    public class When_GetSections_is_called_and_proper_files_are_found : BGReceivedFileServiceTestBase
    {
        private string _downloadFolderPath = @"c:\download";
        private string _customerNumber = "999999";
        private string _productCode = "UAGAG";
        private IEnumerable<string> _fileNames;
        private IEnumerable<string> _returnedFileNames;
        private int counter;

        public When_GetSections_is_called_and_proper_files_are_found()
        {
            Context = () =>
            {
                _fileNames = BGReceivedFileReaderServiceFactory.GetFileNames(_customerNumber, _productCode);
                A.CallTo(() => BGConfigurationSettingsService.GetReceivedFilesFolderPath()).Returns(_downloadFolderPath);
                A.CallTo(() => BGConfigurationSettingsService.GetPaymentRevieverCustomerNumber()).Returns(_customerNumber);
                A.CallTo(() => FileIOService.GetReceivedFileNames(A<string>.Ignored)).Returns(_fileNames);
                A.CallTo(() => FileSplitter.FileNameOk(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(true);
                A.CallTo(() => FileSplitter.GetDateFromName(A<string>.Ignored)).Returns(() => BGReceivedFileReaderServiceFactory.GetDate(counter++)); 
            };
            Because = receivedFileReaderService =>
            {
                _returnedFileNames = receivedFileReaderService.GetFileNames();
            };
        }

        [Test]
        public void Service_calls_filesplitterservice_twice_for_each_filename()
        {
            A.CallTo(() => FileSplitter.FileNameOk(_fileNames.ElementAt(0), _customerNumber, _productCode)).MustHaveHappened();
            A.CallTo(() => FileSplitter.FileNameOk(_fileNames.ElementAt(1), _customerNumber, _productCode)).MustHaveHappened();
            A.CallTo(() => FileSplitter.FileNameOk(_fileNames.ElementAt(2), _customerNumber, _productCode)).MustHaveHappened();
            A.CallTo(() => FileSplitter.FileNameOk(_fileNames.ElementAt(3), _customerNumber, _productCode)).MustHaveHappened();
            A.CallTo(() => FileSplitter.GetDateFromName(_fileNames.ElementAt(0))).MustHaveHappened();
            A.CallTo(() => FileSplitter.GetDateFromName(_fileNames.ElementAt(1))).MustHaveHappened();
            A.CallTo(() => FileSplitter.GetDateFromName(_fileNames.ElementAt(2))).MustHaveHappened();
            A.CallTo(() => FileSplitter.GetDateFromName(_fileNames.ElementAt(3))).MustHaveHappened();
        }

        [Test]
        public void Service_returns_list_of_filenames_ordered_by_date_found_in_filename()
        {
            _returnedFileNames.ElementAt(0).ShouldBe(_fileNames.ElementAt(2));
            _returnedFileNames.ElementAt(1).ShouldBe(_fileNames.ElementAt(0));
            _returnedFileNames.ElementAt(2).ShouldBe(_fileNames.ElementAt(1));
            _returnedFileNames.ElementAt(3).ShouldBe(_fileNames.ElementAt(3));
        }
    }

    [TestFixture, Category("BGReceivedFileReaderServiceTests")]
    public class When_GetSections_is_called : BGReceivedFileServiceTestBase
    {
        private readonly string[] file = new [] {"hello world"};
        public When_GetSections_is_called()
        {
            Context = () => { };
            Because = receivedFileReaderService =>
            {
                receivedFileReaderService.GetSections(file);
            };
        }

        [Test]
        public void Service_calls_filesplitter_service()
        {
            A.CallTo(() => FileSplitter.GetSections(file)).MustHaveHappened();
        }
    }

    [TestFixture, Category("BGReceivedFileReaderServiceTests")]
    public class When_MoveFile_is_called : BGReceivedFileServiceTestBase
    {
        private static string _fileName = "myfile.txt";
        private static string _downloadFolderPath = @"c:\download";
        private static string _backupFolderPath = @"c:\backup";
        private static string _downloadFilePath = @"c:\download\myfile.txt";
        private static string _backupFilePath = @"c:\backup\myfile.txt";
        public When_MoveFile_is_called()
        {
            Context = () =>
            {
                A.CallTo(() => BGConfigurationSettingsService.GetReceivedFilesFolderPath()).Returns(_downloadFolderPath);
                A.CallTo(() => BGConfigurationSettingsService.GetBackupFilesFolderPath()).Returns(_backupFolderPath);  
            };
            Because = receivedFileReaderService =>
            {
                receivedFileReaderService.MoveFile(_fileName);
            };
        }

        [Test]
        public void Service_calls_fileioservice()
        {
            A.CallTo(() => FileIOService.MoveFile(_downloadFilePath, _backupFilePath)).MustHaveHappened();
        }
    }

    [TestFixture, Category("BGReceivedFileReaderServiceTests")]
    public class ReadFileFromDisk : BGReceivedFileServiceTestBase
    {
        private static string _fileName = "myfile.txt";
        private static string _downloadFolderPath = @"c:\download";
        private static string _filePath = @"c:\download\myfile.txt";
       
        public ReadFileFromDisk()
        {
            Context = () =>
            {
                A.CallTo(() => BGConfigurationSettingsService.GetReceivedFilesFolderPath()).Returns(_downloadFolderPath);
            };
            Because = receivedFileReaderService =>
            {
                receivedFileReaderService.ReadFileFromDisk(_fileName);
            };
        }

        [Test]
        public void Service_calls_fileioservice()
        {
            A.CallTo(() => FileIOService.ReadFile(_filePath)).MustHaveHappened();
        }
    }
}
