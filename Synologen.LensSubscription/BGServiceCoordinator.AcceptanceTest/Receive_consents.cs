using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using ReceiveConsentsTask = Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveConsents.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{
	/*Feature: Hämta nya medgivanden

	Scenario: Nya filsektioner av typen medgivande finns sparade i databas
		Verifiera att nya medgivanden sparas i DB
		Verifiera att lästa filsektioner uppdateras som hanterade/lästa
	 */

	[TestFixture, Category("Feature: Receive consents from BGC")]
	public class When_receiving_consents : TaskTestBase
	{
		private ReceiveConsentsTask task;
		private ITaskRunnerService taskRunnerService;
		private ReceivedFileSection consentFileSection;
		private AutogiroPayer payer;

		public When_receiving_consents()
		{
			Context = () =>
			{
				payer = new AutogiroPayer { Name = "Adam Bertil", ServiceType = AutogiroServiceType.LensSubscription };
				autogiroPayerRepository.Save(payer);
				consentFileSection = Factory.GetReceivedConsentFileSection(payer.Id);
				receivedFileRepository.Save(consentFileSection);
				task = ResolveTask<ReceiveConsentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Task_stores_consents()
		{
			var consents = bgReceivedConsentRepository.GetAll().GetLast(7).ToArray();
			consents[0].CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
			consents[0].ConsentValidForDate.ShouldBe(null);

			consents[1].CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
			consents[1].ConsentValidForDate.ShouldBe(new DateTime(2004, 10, 26));

			consents[2].CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
			consents[2].ConsentValidForDate.ShouldBe(new DateTime(2004, 10, 26));

			consents[3].CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
			consents[3].ConsentValidForDate.ShouldBe(new DateTime(2004, 10, 26));

			consents[4].CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
			consents[4].ConsentValidForDate.ShouldBe(new DateTime(2004, 10, 26));

			consents[5].CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
			consents[5].ConsentValidForDate.ShouldBe(null);

			consents[6].CommentCode.ShouldBe(ConsentCommentCode.Canceled);
			consents[6].ConsentValidForDate.ShouldBe(null);

			foreach (var consent in consents)
			{
				consent.ActionDate.ShouldBe(new DateTime(2004, 10, 18));
				consent.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
				consent.Handled.ShouldBe(false);
				consent.InformationCode.Value.ShouldBe(ConsentInformationCode.InitiatedByPaymentRecipient);
				consent.Payer.Id.ShouldBe(payer.Id);
			}
		}

		[Test]
		public void Task_updates_file_sections_as_handled()
		{
			var fetchedConsent = ResolveRepository<IReceivedFileRepository>().Get(consentFileSection.Id);
			fetchedConsent.HandledDate.Value.Date.ShouldBe(DateTime.Now.Date);
			fetchedConsent.HasBeenHandled.ShouldBe(true);
		}
	}
}