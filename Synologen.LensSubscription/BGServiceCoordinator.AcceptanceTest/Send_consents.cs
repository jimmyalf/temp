using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using SendConsentsTask = Synologen.LensSubscription.BGServiceCoordinator.Task.SendConsents.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{
	/*Feature: Skicka nya medgivanden

	Scenario:  Nya medgivanden finns sparade i DB
		Verifiera att filsektioner av typen medgivande sparas
		Verifiera att hanterade medgivanden uppdateras som hanterade/lästa
	 */

	[TestFixture, Category("Feature: Send consents")]
	public class When_sending_consents : TaskTestBase
	{
		private SendConsentsTask task;
		private ITaskRunnerService taskRunnerService;
		private AutogiroPayer payer;
		private BGConsentToSend consent;
		private string expectedFileData;

		public When_sending_consents()
		{
			Context = () =>
			{
				payer = new AutogiroPayer { Name = "Adam Bertil", ServiceType = AutogiroServiceType.LensSubscription };
				autogiroPayerRepository.Save(payer);
				consent = Factory.GetConsentToSend(payer);
				bgConsentToSendRepository.Save(consent);
				expectedFileData = CreateExpectedConsentFileData(consent);

				task = ResolveTask<SendConsentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Task_stores_file_sections()
		{
			var lastFileSectionToSend = fileSectionToSendRepository.GetAll().Last();
			lastFileSectionToSend.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			lastFileSectionToSend.HasBeenSent.ShouldBe(false);
			lastFileSectionToSend.SectionData.ShouldBe(expectedFileData);
			lastFileSectionToSend.SentDate.ShouldBe(null);
			lastFileSectionToSend.Type.ShouldBe(SectionType.ConsentsToSend);
			lastFileSectionToSend.TypeName.ShouldBe(SectionType.ConsentsToSend.GetEnumDisplayName());
		}

		[Test]
		public void Task_updates_consents_as_sent()
		{
			var fetchedConsent = ResolveRepository<IBGConsentToSendRepository>().Get(consent.Id);
			fetchedConsent.SendDate.Value.Date.ShouldBe(DateTime.Now.Date);
		}
	}
}