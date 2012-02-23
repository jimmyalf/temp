using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendFileTaskTests")]
	public class When_sending_file : SendFileTaskTestBase
	{
		private IEnumerable<FileSectionToSend> _fileSections;
		private string _expectedTamperProtectedFileData;
		private string _expectedFtpFileName;

		public When_sending_file()
		{
			Context = () =>
			{
				_fileSections = FileSectionToSendFactory.GetList();
				_expectedFtpFileName = "ftpFileName.txt";
				_expectedTamperProtectedFileData = FileSectionToSendFactory.GenerateTamperProtectedFileData();
				A.CallTo(() => FileSectionToSendRepository.FindBy(A<AllUnhandledFileSectionsToSendCriteria>.Ignored)).
					Returns(_fileSections);
				A.CallTo(() => TamperProtectedFileWriter.Write(A<string>.Ignored)).Returns(_expectedTamperProtectedFileData);
				A.CallTo(() => FtpService.SendFile(A<string>.Ignored)).Returns(new FtpSendResult(_expectedFtpFileName));

			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_has_send_files_ordering()
		{
			Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.SendFiles.ToInteger());
		}

		[Test]
		public void Task_loggs_start_and_stop_messages()
		{
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_fetches_all_new_file_sections_to_send()
		{
			A.CallTo(() => FileSectionToSendRepository.FindBy(A<AllUnhandledFileSectionsToSendCriteria>.Ignored)).MustHaveHappened();
		}

		[Test]
		public void Task_sends_file_over_ftp()
		{
			A.CallTo(() => FtpService.SendFile(_expectedTamperProtectedFileData)).MustHaveHappened();
		}

		[Test]
		public void Task_updates_file_sections_as_sent()
		{
			_fileSections.Each(fileSection => A.CallTo(() => 
				FileSectionToSendRepository.Save(
					A<FileSectionToSend>.That.Matches(x => x.SentDate.HasValue && Equals(x.SentDate.Value.Date, DateTime.Now.Date)))
				).MustHaveHappened());
		}

		[Test]
		public void Task_stores_a_file_copy_of_sent_file_using_same_filename_as_ftp_service()
		{
			A.CallTo(() => FileWriterService.WriteFileToDisk(_expectedTamperProtectedFileData, _expectedFtpFileName)).MustHaveHappened();
		}
	}

	[TestFixture, Category("SendFileTaskTests")]
	public class When_sending_file_and_no_new_file_sections_exist : SendFileTaskTestBase
	{
		
		public When_sending_file_and_no_new_file_sections_exist()
		{
			Context = () => { };
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_loggs_found_no_new_file_sections_to_send_message()
		{
			LoggingService.AssertInfo("Found no new file sections to send.");
		}
	}
}