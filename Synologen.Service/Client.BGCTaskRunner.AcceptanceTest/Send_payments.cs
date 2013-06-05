using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.SendPayments;
using Synologen.Service.Client.BGCTaskRunner.AcceptanceTest.TestHelpers;

namespace Synologen.Service.Client.BGCTaskRunner.AcceptanceTest
{
	/*Feature: Skicka nya betalningar

	Scenario:  Nya betalningar finns sparade i DB
		Verifiera att filsektioner av typen betalningar sparas
		Verifiera att hanterade betalningar uppdateras som hanterade/lästa
	 */

	[TestFixture, Category("Feature: Send payments")]
	public class When_sending_payments : TaskTestBase
	{
		private Task task;
		private ITaskRunnerService taskRunnerService;
		private AutogiroPayer payer;
		private BGPaymentToSend payment;
		private string expectedFileData;

		public When_sending_payments()
		{
			Context = () =>
			{
				payer = new AutogiroPayer { Name = "Adam Bertil", ServiceType = AutogiroServiceType.LensSubscription };
				autogiroPayerRepository.Save(payer);
				payment = Factory.GetPaymentToSend(payer);
				bgPaymentToSendRepository.Save(payment);
				expectedFileData = CreateExpectedPaymentFileData(payment);

				task = ResolveTask<Task>();
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
			lastFileSectionToSend.Type.ShouldBe(SectionType.PaymentsToSend);
			lastFileSectionToSend.TypeName.ShouldBe(SectionType.PaymentsToSend.GetEnumDisplayName());
		}

		[Test]
		public void Task_updates_consents_as_sent()
		{
			var fetchedPayment = ResolveRepository<IBGPaymentToSendRepository>().Get(payment.Id);
			fetchedPayment.SendDate.Value.Date.ShouldBe(DateTime.Now.Date);
			fetchedPayment.HasBeenSent.ShouldBe(true);
		}
	}
}