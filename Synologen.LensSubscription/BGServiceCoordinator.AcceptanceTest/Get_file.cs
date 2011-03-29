using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using GetFileTask = Synologen.LensSubscription.BGServiceCoordinator.Task.GetFile.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{

	/* Feature: Get new files from BGC

	Scenario: New file is available
		Verify expected file sections with expected type are stored in DB
		Verify read file is moved after task has been run
	 */

	[TestFixture, Category("Feature: Get new files from BGC")]
	public class When_fetching_new_files : TaskTestBase
	{
		private GetFileTask task;
		private ITaskRunnerService taskRunnerService;
		private ReceivedFileSection[] lastStoredFileSections;
		private string filePath;

		public When_fetching_new_files()
		{
			Context = () =>
			{
				ClearFoldersOnDisk();
				var fileContents = Factory.ConcatenateFileSections(Factory.GetReceivedConsentData, Factory.GetReceivedErrorData, Factory.GetReceivedPaymentData);
				filePath = StoreNewBGCFileOnDisk(fileContents);
				task = ResolveTask<GetFileTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
				lastStoredFileSections = receivedFileRepository.GetAll().GetLast(3).ToArray();
			};
		}

		[Test]
		public void Task_stores_new_file_sections()
		{
			lastStoredFileSections[0].SectionData.ShouldBe(Factory.GetReceivedConsentData());
			lastStoredFileSections[0].Type.ShouldBe(SectionType.ReceivedConsents);
			lastStoredFileSections[1].SectionData.ShouldBe(Factory.GetReceivedErrorData());
			lastStoredFileSections[1].Type.ShouldBe(SectionType.ReceivedErrors);
			lastStoredFileSections[2].SectionData.ShouldBe(Factory.GetReceivedPaymentData());
			lastStoredFileSections[2].Type.ShouldBe(SectionType.ReceivedPayments);

			foreach (var section in lastStoredFileSections)
			{
				section.HandledDate.ShouldBe(null);
				section.HasBeenHandled.ShouldBe(false);
				section.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			}
		}

		[Test]
		public void Task_moves_read_file()
		{
			System.IO.File.Exists(filePath).ShouldBe(false);
			var expectedNewFilePath = GetReadFilePath();
			System.IO.File.Exists(expectedNewFilePath).ShouldBe(true);
		}

	}
}