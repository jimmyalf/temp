using System;
using System.Collections.Generic;
using FakeItEasy;
using Spinit.Extensions;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Test.Factories;
using Synologen.LensSubscription.Autogiro.Test.TestHelpers;

namespace Synologen.LensSubscription.Autogiro.Test
{
    [TestFixture, Category("FileSplitterTester")]
    public class When_GetDateFromName_is_called : FileSplitterTesteBase
    {
        private IEnumerable<string>_fileNames;
        private List<DateTime> _extractedDates;
        private readonly List<DateTime> _dates = new List<DateTime>();
		private const string fileNameRegexPattern = @"BFEP\.{ProductCode}\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";
        
        public When_GetDateFromName_is_called()
        {
             _fileNames = FileSplitterFactory.GetFileNames();
             _extractedDates = FileSplitterFactory.GetDates();
			A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
            _fileNames.Each(fileName => _dates.Add(fileSplitter.GetDateFromName(fileName)));
        }

        [Test]
        public void Dates_are_extracted_from_filenames()
        {
            _dates.ForBoth(_extractedDates, (date, extractedDate) => extractedDate.ShouldBe(date));
        }
    }

    [TestFixture, Category("FileSplitterTester")]
    public class When_FileNameOk_is_called : FileSplitterTesteBase
    {
        private IEnumerable<string> _fileNames;
        private List<bool> _results = new List<bool>();
        private readonly List<bool> _expectedResults;
        private readonly string _customerNumber = "999999";
        private readonly string _productCode = "UAGAG";
		private const string fileNameRegexPattern = @"BFEP\.({ProductCode}|UAGU4)\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";

        public When_FileNameOk_is_called()
        {
            _fileNames = FileSplitterFactory.GetValidAndInvalidFileNames();
            _expectedResults = FileSplitterFactory.GetBooleanResults();
        	A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
            _fileNames.Each(fileName => _results.Add(fileSplitter.FileNameOk(fileName, _customerNumber, _productCode)));
        }

        [Test]
        public void Filenames_are_parsed_and_checked()
        {
            _expectedResults.ForBoth(_results, (expectedResult, result) => result.ShouldBe(expectedResult));
        }
    }

    [TestFixture, Category("FileSplitterTester")]
    public class When_GetSections_is_called_and_file_is_empty : FileSplitterTesteBase
    {
        private Exception _exception;
		private const string fileNameRegexPattern = @"BFEP\.{ProductCode}\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";
        
        public When_GetSections_is_called_and_file_is_empty()
        {
            A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
            try
            {
                fileSplitter.GetSections(new string[] { });
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Test]
        public void Exception_is_thrown()
        {
            _exception.GetType().ShouldBe(typeof(AutogiroFileSplitException));
            _exception.Message.ShouldBe("File was empty");
        }
    }

    [TestFixture, Category("FileSplitterTester")]
    public class When_GetSections_is_called_and_file_does_not_start_with_correct_opening_type : FileSplitterTesteBase
    {
        private Exception _exception;
        private string[] _file;
		private const string fileNameRegexPattern = @"BFEP\.{ProductCode}\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";
        public When_GetSections_is_called_and_file_does_not_start_with_correct_opening_type()
        {
            try
            {
				A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
                _file = FileSplitterFactory.GetFileWithoutOpeningType();
                fileSplitter.GetSections(_file);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Test]
        public void Exception_is_thrown()
        {
            _exception.GetType().ShouldBe(typeof(AutogiroFileSplitException));
            _exception.Message.ShouldBe("Did not find expected opening post type TK01");
        }
    }

    [TestFixture, Category("FileSplitterTester")]
    public class When_GetSections_is_called_and_file_does_not_end_with_correct_type : FileSplitterTesteBase
    {
        private Exception _exception;
        private string[] _file;
		private const string fileNameRegexPattern = @"BFEP\.{ProductCode}\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";
        public When_GetSections_is_called_and_file_does_not_end_with_correct_type()
        {
            try
            {
				A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
                _file = FileSplitterFactory.GetFileWithoutEndingType();
                fileSplitter.GetSections(_file);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Test]
        public void Exception_is_thrown()
        {
            _exception.GetType().ShouldBe(typeof(AutogiroFileSplitException));
            _exception.Message.ShouldBe("File did not end with expected post type TK09");
        }
    }

    [TestFixture, Category("FileSplitterTester")]
    public class When_GetSections_is_called_and_file_contain_unknown_section_type : FileSplitterTesteBase
    {
        private Exception _exception;
        private string[] _file;
        private static string _line = "0120041015AUTOGIRO00000000000000000000000000000000000000                        ";
		private const string fileNameRegexPattern = @"BFEP\.{ProductCode}\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";
        public When_GetSections_is_called_and_file_contain_unknown_section_type()
        {
            try
            {
				A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
                _file = FileSplitterFactory.GetFileWithUnkownSectionType();
                fileSplitter.GetSections(_file);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Test]
        public void Exception_is_thrown()
        {
            _exception.GetType().ShouldBe(typeof(AutogiroFileSplitException));
            _exception.Message.ShouldBe(string.Format("Could not determine file section type: {0}", _line));
        }
    }

    [TestFixture, Category("FileSplitterTester")]
    public class When_GetSections_is_called_and_file_contains_multiple_sections : FileSplitterTesteBase
    {
        
        private string[] _file;
        private List<FileSection> _sections;
        private IEnumerable<FileSection> _expectedSections;
		private const string fileNameRegexPattern = @"BFEP\.{ProductCode}\.K0{CustomerNumber}\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$";

        public When_GetSections_is_called_and_file_contains_multiple_sections()
        {
			A.CallTo(() => bgServiceCoordinatorSettingsService.ReceiveFileNameRegexPattern()).Returns(fileNameRegexPattern);
            _file = FileSplitterFactory.GetFileWithMultipleSections();
            _expectedSections = FileSplitterFactory.GetExpectedSections();
            _sections = (List<FileSection>) fileSplitter.GetSections(_file);
        }

        [Test]
        public void Multiple_sections_is_found()
        {
            _sections.Count.ShouldBe(3);
        }

        [Test]
        public void Sections_are_returned_with_correct_posts_and_type_()
        {
            _expectedSections.ForBoth(_sections, (expectedSection, section) => 
                    section.Posts.ShouldBe(expectedSection.Posts));

            _expectedSections.ForBoth(_sections, (expectedSection, section) =>
                    section.SectionType.ShouldBe(expectedSection.SectionType));
 
        }
    }
}
