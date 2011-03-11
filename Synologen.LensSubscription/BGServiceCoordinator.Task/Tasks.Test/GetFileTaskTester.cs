using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
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
            A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
            A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
        }

        [Test]
        public void Task_calls_fileservice_to_read_from_disk()
        {
            A.CallTo(() => FileReaderService.GetFileNames()).MustHaveHappened();
        }

        [Test]
        public void Task_loggs_that_no_file_was_found()
        {
            A.CallTo(() => Log.Debug(A<string>.That.Contains("No received files found"))).MustHaveHappened();
        }

    }

    [TestFixture, Category("GetFileTaskTests")]
    public class When_getting_a_file : GetFileTaskTestBase
    {
        private readonly IEnumerable<string> fileNames = GetFileFactory.GetFileNames();
        private readonly string[] file = GetFileFactory.GetFile();
        private readonly IEnumerable<FileSection> sections = GetFileFactory.GetSections();

        public When_getting_a_file()
        {
            Context = () =>
            {
                A.CallTo(() => FileReaderService.GetFileNames()).Returns(fileNames);
                A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.Ignored)).Returns(file);
                A.CallTo(() => FileReaderService.GetSections(A<string[]>.Ignored)).Returns(sections);
            };
            Because = task => task.Execute(ExecutingTaskContext);
        }


        [Test]
        public void Task_gets_the_list_of_file_names()
        {
            A.CallTo(() => FileReaderService.GetFileNames()).MustHaveHappened(Repeated.Once);
        }

        [Test]
        public void Task_loggs_number_of_found_files()
        {
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Found {0} files", fileNames.Count())))).MustHaveHappened();
        }

        [Test]
        public void Task_calls_fileservice_for_a_file()
        {
            A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.That.Contains(fileNames.ElementAt(0)))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.That.Contains(fileNames.ElementAt(1)))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => FileReaderService.ReadFileFromDisk(A<string>.That.Contains(fileNames.ElementAt(2)))).MustHaveHappened(Repeated.Once);
        }

        [Test]
        public void Task_calls_fileservice_for_sections()
        {
            A.CallTo(() => FileReaderService.GetSections(file)).MustHaveHappened(Repeated.Times(3));
        }

        [Test]
        public void Task_loggs_number_of_found_sections()
        {
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Found {0} sections in file {1}", sections.Count(), fileNames.ElementAt(0))))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Found {0} sections in file {1}", sections.Count(), fileNames.ElementAt(1))))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Found {0} sections in file {1}", sections.Count(), fileNames.ElementAt(2))))).MustHaveHappened(Repeated.Once);
        }

        [Test]
        public void Task_saves_section_to_repository()
        {
            A.CallTo(() => ReceivedFileRepository.Save(A<ReceivedFileSection>
                    .That.Matches(x => x.SectionData.Equals(sections.ElementAt(0).Posts))
                    .And.Matches(x => x.Type.Equals(sections.ElementAt(0).SectionType))
                    .And.Matches(x => x.TypeName.Equals(sections.ElementAt(0).SectionType.GetEnumDisplayName()))
                    .And.Matches(x => x.CreatedDate.Date.Equals(DateTime.Now.Date)))).MustHaveHappened(Repeated.Times(3));

            A.CallTo(() => ReceivedFileRepository.Save(A<ReceivedFileSection>
                    .That.Matches(x => x.SectionData.Equals(sections.ElementAt(1).Posts))
                    .And.Matches(x => x.Type.Equals(sections.ElementAt(1).SectionType))
                    .And.Matches(x => x.TypeName.Equals(sections.ElementAt(1).SectionType.GetEnumDisplayName()))
                    .And.Matches(x => x.CreatedDate.Date.Equals(DateTime.Now.Date)))).MustHaveHappened(Repeated.Times(3));
            
            A.CallTo(() => ReceivedFileRepository.Save(A<ReceivedFileSection>
                    .That.Matches(x => x.SectionData.Equals(sections.ElementAt(2).Posts))
                    .And.Matches(x => x.Type.Equals(sections.ElementAt(2).SectionType))
                    .And.Matches(x => x.TypeName.Equals(sections.ElementAt(2).SectionType.GetEnumDisplayName()))
                    .And.Matches(x => x.CreatedDate.Date.Equals(DateTime.Now.Date)))).MustHaveHappened(Repeated.Times(3));
        }

        [Test]
        public void Task_loggs_number_of_saved_sections()
        {
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Saved {0} sections from file {1}", sections.Count(), fileNames.ElementAt(0))))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Saved {0} sections from file {1}", sections.Count(), fileNames.ElementAt(1))))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => Log.Debug(A<string>.That.Contains(string.Format("Saved {0} sections from file {1}", sections.Count(), fileNames.ElementAt(2))))).MustHaveHappened(Repeated.Once);
        }

        [Test]
        public void Task_moves_all_files_to_a_backup_folder()
        {
            A.CallTo(() => FileReaderService.MoveFile(fileNames.ElementAt(0))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => FileReaderService.MoveFile(fileNames.ElementAt(1))).MustHaveHappened(Repeated.Once);
            A.CallTo(() => FileReaderService.MoveFile(fileNames.ElementAt(2))).MustHaveHappened(Repeated.Once);
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
            A.CallTo(() => Log.Error(A<string>.That.Contains("Error when moving read file to backup folder"))).MustHaveHappened();
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
            A.CallTo(() => Log.Error(A<string>.That.Contains(string.Format("Exception when parsing and splitting file {0}", fileNames.ElementAt(0))))).MustHaveHappened();
        }
    }
}
