using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture]
	public class When_sending_file : SendFileTaskTestBase
	{
		private IEnumerable<FileSectionToSend> fileSections;
		private string expectedFileDataWithOpeningTamperProtectionRow;

		public When_sending_file()
		{
			Context = () =>
			{
				fileSections = FileSectionToSendFactory.GetList();
				expectedFileDataWithOpeningTamperProtectionRow = FileSectionToSendFactory.GetFileDataWithOpeningTamperProtectionRow(fileSections, WriteDate);
				A.CallTo(() => FileSectionToSendRepository.FindBy(A<AllUnhandledFileSectionsToSendCriteria>.Ignored.Argument)).
					Returns(fileSections);
				A.CallTo(() => HashService.CondensateTypeName).Returns("HMAC");

			};
			Because = task => task.Execute();
		}

		[Test]
		public void Task_has_send_files_ordering()
		{
			Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.SendFiles.ToInteger());
		}

		[Test]
		public void Task_loggs_start_and_stop_messages()
		{
			A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
			A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
		}

		[Test]
		public void Task_fetches_all_new_file_sections_to_send()
		{
			A.CallTo(() => FileSectionToSendRepository.FindBy(A<AllUnhandledFileSectionsToSendCriteria>.Ignored.Argument)).MustHaveHappened();
		}

		[Test]
		public void Task_creates_message_authentication_code_value_hash()
		{
			A.CallTo(() => HashService.GetHash(TamperProtectedFileWriter.KeyVerificationToken)).MustHaveHappened();
		}

		[Test]
		public void Task_creates_key_verification_value_hash()
		{
			A.CallTo(() => HashService.GetHash(expectedFileDataWithOpeningTamperProtectionRow)).MustHaveHappened();
		}
	}

	[TestFixture]
	public class When_sending_file_and_no_new_file_sections_exist : SendFileTaskTestBase
	{
		
		public When_sending_file_and_no_new_file_sections_exist()
		{
			Context = () => { };
			Because = task => task.Execute();
		}

		[Test]
		public void Task_loggs_found_no_new_file_sections_to_send_message()
		{
			A.CallTo(() => Log.Info(A<string>.That.Contains("Found no new file sections to send."))).MustHaveHappened();
		}

		[Test]
		public void Task_does_not_create_any_hashes()
		{
			A.CallTo(() => HashService.GetHash(A<string>.Ignored)).MustNotHaveHappened();
		}
	}
}