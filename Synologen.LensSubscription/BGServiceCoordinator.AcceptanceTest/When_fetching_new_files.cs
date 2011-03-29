using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using GetFileTask = Synologen.LensSubscription.BGServiceCoordinator.Task.GetFile.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{

	/* Feature: Get new files from BGC

	Scenario: New file is available
		Verify expected file sections are stored in DB
		Verify saved sections have expected type
		Verify read file is moved after task has been run
	 */

	[TestFixture, Category("Feature: Get new files from BGC")]
	public class When_fetching_new_files : TaskTestBase
	{
		private GetFileTask task;
		private ITaskRunnerService taskRunnerService;

		public When_fetching_new_files()
		{
			Context = () =>
			{
				var fileContents = Factory.ConcatenateFileSections(Factory.GetReceivedConsentFileSection, Factory.GetReceivedErrorFileSection, Factory.GetReceivedPaymentFileSection);
				StoreNewBGCFileOnDisk(fileContents);
				task = ResolveTask<GetFileTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Test()
		{
			var lastStoredFileSections = receivedFileRepository.GetAll().GetLast(3).ToArray();
			lastStoredFileSections[0].SectionData.ShouldBe(Factory.GetReceivedConsentFileSection());
			lastStoredFileSections[1].SectionData.ShouldBe(Factory.GetReceivedErrorFileSection());
			lastStoredFileSections[2].SectionData.ShouldBe(Factory.GetReceivedPaymentFileSection());
		}
	}
}