using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using ReceivePaymentsTask = Synologen.LensSubscription.BGServiceCoordinator.Task.ReceivePayments.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{

	/*Feature: Hämta nya betalningar

	Scenario: Nya filsektioner av typen betalningar finns sparade i databas
		Verifiera att nya betalningar sparas i DB
		Verifiera att lästa filsektioner uppdateras som hanterade/lästa
	 */

	[TestFixture, Category("Feature: Receive payments from BGC")]
	public class When_receiving_payments : TaskTestBase
	{
		private AutogiroPayer payer;
		private ReceivePaymentsTask task;
		private ITaskRunnerService taskRunnerService;
		private ReceivedFileSection paymentFileSection;

		public When_receiving_payments()
		{
			Context = () =>
			{
				payer = new AutogiroPayer { Name = "Adam Bertil", ServiceType = AutogiroServiceType.LensSubscription };
				autogiroPayerRepository.Save(payer);
				paymentFileSection = Factory.GetReceivedPaymentFileSection(payer.Id);
				receivedFileRepository.Save(paymentFileSection);
				task = ResolveTask<ReceivePaymentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Task_stores_payments()
		{
			var payments = bgReceivedPaymentRepository.GetAll().GetLast(15).ToArray();

			payments.ForElementAtIndex(0, payment =>
			{
				payment.Amount.ShouldBe(243);
				payment.Reference.ShouldBe("0809001");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(1, payment =>
			{
				payment.Amount.ShouldBe(384);
				payment.Reference.ShouldBe("0809002");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(2, payment =>
			{
				payment.Amount.ShouldBe(335);
				payment.Reference.ShouldBe("0809004");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(3, payment =>
			{
				payment.Amount.ShouldBe(462);
				payment.Reference.ShouldBe("0809005");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(4, payment =>
			{
				payment.Amount.ShouldBe(172);
				payment.Reference.ShouldBe("0809006");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(5, payment =>
			{
				payment.Amount.ShouldBe(484);
				payment.Reference.ShouldBe("0809007");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(6, payment =>
			{
				payment.Amount.ShouldBe(314);
				payment.Reference.ShouldBe("0809008");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(7, payment =>
			{
				payment.Amount.ShouldBe(112);
				payment.Reference.ShouldBe("0809009");	
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(8, payment =>
			{
				payment.Amount.ShouldBe(487);
				payment.Reference.ShouldBe("0809010");	
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});

			payments.ForElementAtIndex(9, payment =>
			{
				payment.Amount.ShouldBe(434);
				payment.Reference.ShouldBe("0809011");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});


			payments.ForElementAtIndex(10, payment =>
			{
				payment.Amount.ShouldBe(337);
				payment.Reference.ShouldBe("0809012");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Debit);
			});
			

			payments.ForElementAtIndex(11, payment =>
			{
				payment.Amount.ShouldBe(16874);
				payment.Reference.ShouldBe("0809745");
				payment.ResultType.ShouldBe(PaymentResult.Approved);
				payment.Type.ShouldBe(PaymentType.Credit);
			});
			

			payments.ForElementAtIndex(12, payment =>
			{
				payment.Amount.ShouldBe(253);
				payment.Reference.ShouldBe("0809013");
				payment.ResultType.ShouldBe(PaymentResult.AGConnectionMissing);
				payment.Type.ShouldBe(PaymentType.Debit);
			});
			

			payments.ForElementAtIndex(13, payment =>
			{
				payment.Amount.ShouldBe(969);
				payment.Reference.ShouldBe("0809014");
				payment.ResultType.ShouldBe(PaymentResult.InsufficientFunds);
				payment.Type.ShouldBe(PaymentType.Debit);
			});
			

			payments.ForElementAtIndex(14, payment =>
			{
				payment.Amount.ShouldBe(489);
				payment.Reference.ShouldBe("0809015");
				payment.ResultType.ShouldBe(PaymentResult.WillTryAgain);
				payment.Type.ShouldBe(PaymentType.Debit);
			});
			

			foreach (var payment in payments)
			{
				payment.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
				payment.Handled.ShouldBe(false);
				payment.NumberOfReoccuringTransactionsLeft.ShouldBe(null);
				payment.Payer.Id.ShouldBe(payer.Id);
				payment.PaymentDate.ShouldBe(new DateTime(2004, 10, 28));
				payment.PeriodCode.ShouldBe(PaymentPeriodCode.PaymentOnceOnSelectedDate);
				payment.Reciever.CustomerNumber.ShouldBe("471117");
				payment.Reciever.BankgiroNumber.ShouldBe("9912346");
			}
		}

		[Test]
		public void Task_updates_file_sections_as_handled()
		{
			var fetchedPayment = ResolveRepository<IReceivedFileRepository>().Get(paymentFileSection.Id);
			fetchedPayment.HandledDate.Value.Date.ShouldBe(DateTime.Now.Date);
			fetchedPayment.HasBeenHandled.ShouldBe(true);
		}

	}
}