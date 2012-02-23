using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
    
    [TestFixture, Category("GetFileTaskTests")]
    public class When_getting_no_file : GetFileTaskTestBase
    {
        
        public When_getting_no_file()
        {
            Context = () =>
            {
                A.CallTo(() => FileReaderService.GetFileNames()).Returns(Enumerable.Empty<string>());
            };
            Because = task => task.Execute(ExecutingTaskContext);
        }

        [Test]
        public void Task_has_fetch_files_ordering()
        {
            Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.FetchFiles.ToInteger());
        }

        [Test]
        public void Task_loggs_start_and_stop_messages()
        {
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
        }

        [Test]
        public void Task_calls_fileservice_to_read_from_disk()
        {
            A.CallTo(() => FileReaderService.GetFileNames()).MustHaveHappened();
        }

        [Test]
        public void Task_loggs_that_no_file_was_found()
        {
        	LoggingService.AssertDebug("No received files found");
        }

    }

    [TestFixture, Category("GetFileTaskTests")]
    public class When_getting_a_file : GetFileTaskTestBase
    {
    	private IEnumerable<string> _fileNames;
        private string[] _file;
        private IEnumerable<FileSection> _sections;

        public When_getting_a_file()
        {
            Context = () =>
            {
				_fileNames = GetFileFactory.GetFileNames();
				_file = GetFileFactory.GetFile();
				_sections = GetFileFactory.GetSections();
                A.CallTo(() => FileReaderService.GetFileNames()).Returns(_fileNames);
                A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.Ignored)).Returns(_file);
                A.CallTo(() => FileReaderService.GetSections(A<string[]>.Ignored)).Returns(_sections);
            };
            Because = task => task.Execute(ExecutingTaskContext);
        }


        [Test]
        public void Task_gets_the_list_of_file_names()
        {
            A.CallTo(() => FileReaderService.GetFileNames()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Task_loggs_number_of_found_files()
        {
			LoggingService.AssertDebug("Found {0} files", _fileNames.Count());
        }

        [Test]
        public void Task_calls_fileservice_for_a_file()
        {
			_fileNames.Each(fileName => 
				A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.That.Contains(fileName)))
				.MustHaveHappened(Repeated.Exactly.Once)
			);
        }

        [Test]
        public void Task_calls_fileservice_for_sections()
        {
            A.CallTo(() => FileReaderService.GetSections(_file)).MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [Test]
        public void Task_saves_section_to_repository()
        {
        	_sections.Each(fileSection => A.CallTo(() => ReceivedFileRepository.Save(A<ReceivedFileSection>.That.Matches(x => 
				x.SectionData.Equals(fileSection.Posts)
				&& x.Type.Equals(fileSection.SectionType)
				&& x.TypeName.Equals(fileSection.SectionType.GetEnumDisplayName())
				&& x.CreatedDate.Date.Equals(DateTime.Now.Date)
			))).MustHaveHappened(Repeated.Exactly.Times(3)));
        }

        [Test]
        public void Task_loggs_number_of_found_sections()
        {
			foreach (var fileName in _fileNames)
			{
				LoggingService.AssertDebug("Found {0} sections in file {1}", _sections.Count(), fileName);
			}
        }

        [Test]
        public void Task_loggs_number_of_saved_sections()
        {
        	foreach (var fileName in _fileNames)
        	{
        		LoggingService.AssertDebug("Saved {0} sections from file {1}", _sections.Count(), fileName);
        	}
        }

        [Test]
        public void Task_moves_all_files_to_a_backup_folder()
        {
        	_fileNames.Each(fileName => 
				A.CallTo(() => FileReaderService.MoveFile(fileName))
				.MustHaveHappened(Repeated.Exactly.Once)
			);
        }
    }

    [TestFixture, Category("GetFileTaskTests")]
    public class When_Filereaderservice_MoveFile_throws_exception : GetFileTaskTestBase
    {
        private readonly IEnumerable<string> fileNames = GetFileFactory.GetFileName();
        private readonly string[] file = GetFileFactory.GetFile();
        private readonly IEnumerable<FileSection> sections = GetFileFactory.GetSections();

        public When_Filereaderservice_MoveFile_throws_exception()
        {
            Context = () =>
            {
                A.CallTo(() => FileReaderService.GetFileNames()).Returns(fileNames);
                A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.Ignored)).Returns(file);
                A.CallTo(() => FileReaderService.GetSections(A<string[]>.Ignored)).Returns(sections);
                A.CallTo(() => FileReaderService.MoveFile(A<string>.Ignored)).Throws(new Exception());
            };
            Because = task => task.Execute(ExecutingTaskContext);
        }

        [Test]
        public void Task_logs_error()
        {
        	LoggingService.AssertError<Exception>("Error when moving read file to backup folder");
        }
    }

    [TestFixture, Category("GetFileTaskTests")]
    public class When_Filereaderservice_GetSections_throws_exception : GetFileTaskTestBase
    {
        private readonly IEnumerable<string> fileNames = GetFileFactory.GetFileName();
        private readonly string[] file = GetFileFactory.GetFile();

        public When_Filereaderservice_GetSections_throws_exception()
        {
            Context = () =>
            {
                A.CallTo(() => FileReaderService.GetFileNames()).Returns(fileNames);
                A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.Ignored)).Returns(file);
                A.CallTo(() => FileReaderService.GetSections(A<string[]>.Ignored)).Throws(new AutogiroFileSplitException());
            };
            Because = task => task.Execute(ExecutingTaskContext);
        }

        [Test]
        public void Task_logs_error()
        {
        	LoggingService.AssertError<AutogiroFileSplitException>("Exception when parsing and splitting file {0}", fileNames.First());
        }
    }
}
