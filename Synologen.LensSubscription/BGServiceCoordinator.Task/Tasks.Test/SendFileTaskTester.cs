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
		private IEnumerable<FileSectionToSend> fileSections;
		private string expectedTamperProtectedFileData;
		private string expectedFtpFileName;

		public When_sending_file()
		{
			Context = () =>
			{
				fileSections = FileSectionToSendFactory.GetList();
				expectedFtpFileName = "ftpFileName.txt";
				expectedTamperProtectedFileData = FileSectionToSendFactory.GenerateTamperProtectedFileData();
				A.CallTo(() => FileSectionToSendRepository.FindBy(A<AllUnhandledFileSectionsToSendCriteria>.Ignored.Argument)).
					Returns(fileSections);
				A.CallTo(() => TamperProtectedFileWriter.Write(A<string>.Ignored)).Returns(expectedTamperProtectedFileData);
				A.CallTo(() => FtpService.SendFile(A<string>.Ignored)).Returns(new FtpSendResult(expectedFtpFileName));

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
			A.CallTo(() => FileSectionToSendRepository.FindBy(A<AllUnhandledFileSectionsToSendCriteria>.Ignored.Argument)).MustHaveHappened();
		}

		[Test]
		public void Task_sends_file_over_ftp()
		{
			A.CallTo(() => FtpService.SendFile(expectedTamperProtectedFileData)).MustHaveHappened();
		}

		[Test]
		public void Task_updates_file_sections_as_sent()
		{
			fileSections.Each(fileSection => A.CallTo(() => 
				FileSectionToSendRepository.Save(
					A<FileSectionToSend>.That.Matches(x => x.SentDate.HasValue && Equals(x.SentDate.Value.Date, DateTime.Now.Date)))
				).MustHaveHappened());
		}

		[Test]
		public void Task_stores_a_file_copy_of_sent_file_using_same_filename_as_ftp_service()
		{
			A.CallTo(() => FileWriterService.WriteFileToDisk(expectedTamperProtectedFileData, expectedFtpFileName)).MustHaveHappened();
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