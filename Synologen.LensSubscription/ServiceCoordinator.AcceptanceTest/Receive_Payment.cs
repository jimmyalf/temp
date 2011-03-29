using System;
using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest;
using ServiceCoordinator.AcceptanceTest.TestHelpers;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGData.Repositories;
using ReceivePaymentsTask = Synologen.LensSubscription.ServiceCoordinator.Task.ReceivePayments.Task;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Payment")]
	public class When_receiveing_a_successful_payment : ReceivePaymentTaskbase
	{
		private ReceivePaymentsTask task;
		ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Subscription subscription;
		private BGReceivedPayment successfulPayment;

		public When_receiveing_a_successful_payment()
		{
			Context = () =>
			{
				bankGiroPayerNumber = RegisterPayerWithWebService();
				subscription = StoreSubscription(customer => Factory.CreateSubscriptionReadyForPayment(customer, bankGiroPayerNumber), bankGiroPayerNumber);
				successfulPayment = StoreBGPayment(Factory.CreateSuccessfulPayment, bankGiroPayerNumber);

				task = ResolveTask<ReceivePaymentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}


		[Test]
		public void Task_creates_a_transaction()
		{
			var fetchedSubscription = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription.Id);
			fetchedSubscription.Transactions.Count().ShouldBe(1);
			var newTransaction = fetchedSubscription.Transactions.First();
			newTransaction.Amount.ShouldBe(successfulPayment.Amount);
			newTransaction.Article.ShouldBe(null);
			newTransaction.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			newTransaction.Reason.ShouldBe(TransactionReason.Payment);
			newTransaction.Settlement.ShouldBe(null);
			newTransaction.Type.ShouldBe(TransactionType.Deposit);
		}

		[Test]
		public void Task_updates_recieved_payment_as_handled()
		{
			var payment = new BGReceivedPaymentRepository(GetBGSession()).Get(successfulPayment.Id);
			payment.Handled.ShouldBe(true);
		}
	}

	[TestFixture, Category("Feature: Receiving Payment")]
	public class When_receiveing_a_failed_payment : ReceivePaymentTaskbase
	{
		private ReceivePaymentsTask task;
		ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Subscription subscription;
		private BGReceivedPayment failedPayment;

		public When_receiveing_a_failed_payment()
		{
			Context = () =>
			{
				bankGiroPayerNumber = RegisterPayerWithWebService();
				subscription = StoreSubscription(customer => Factory.CreateSubscriptionReadyForPayment(customer, bankGiroPayerNumber), bankGiroPayerNumber);
				failedPayment = StoreBGPayment(Factory.CreateFailedPayment, bankGiroPayerNumber);

				task = ResolveTask<ReceivePaymentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}


		[Test]
		public void Task_does_not_create_a_transaction()
		{
			var fetchedSubscription = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription.Id);
			fetchedSubscription.Transactions.Count().ShouldBe(0);
		}

		[Test]
		public void Task_creates_a_subscription_error()
		{
			var lastError = subscriptionErrorRepository.GetAll().Last();
			lastError.BGPaymentId.ShouldBe(failedPayment.Id);
			lastError.Code.ShouldBe(null);
			lastError.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			lastError.HandledDate.ShouldBe(null);
			lastError.IsHandled.ShouldBe(false);
			lastError.Subscription.Id.ShouldBe(subscription.Id);
			lastError.Type.ShouldBe(SubscriptionErrorType.PaymentRejectedInsufficientFunds);
		}

		[Test]
		public void Task_updates_recieved_payment_as_handled()
		{
			var payment = new BGReceivedPaymentRepository(GetBGSession()).Get(failedPayment.Id);
			payment.Handled.ShouldBe(true);
		}
	}
}