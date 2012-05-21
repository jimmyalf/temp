using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.ServiceCoordinator.Task.ReceivePayments;
using Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest.TestHelpers;

namespace Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Payment")]
	public class When_receiveing_a_successful_payment : TaskBase
	{
		private Task _task;
		ITaskRunnerService _taskRunnerService;
		private int _bankGiroPayerNumber;
		private Subscription _subscription;
		private BGReceivedPayment _successfulPayment;
		private IEnumerable<SubscriptionItem> _subscriptionItems;
		private SubscriptionPendingPayment _pendingPayment;

		public When_receiveing_a_successful_payment()
		{
			Context = () =>
			{
				_bankGiroPayerNumber = RegisterPayerWithWebService();
				var shop = CreateShop<Shop>();
				_subscription = StoreSubscription(customer => Factory.CreateSubscription(customer, shop, _bankGiroPayerNumber, SubscriptionConsentStatus.Accepted, new DateTime(2011,01,01)), shop.Id);
				_subscriptionItems = StoreItemsWithWpcSession(() => Factory.CreateSubscriptionItems(_subscription));
				_pendingPayment = StoreWithWpcSession(() => Factory.CreatePendingPayment(_subscriptionItems.ToList()));
				_successfulPayment = StoreBGPayment(getPayment => Factory.CreateSuccessfulPayment(getPayment, _pendingPayment.Id.ToString(), _pendingPayment.Amount), _bankGiroPayerNumber);
				_task = ResolveTask<Task>();
				_taskRunnerService = GetTaskRunnerService(_task);
			};
			Because = () => _taskRunnerService.Run();
		}


		[Test]
		public void PendingPayment_is_updated()
		{
			var pendingPayment = Get<SubscriptionPendingPayment>(GetWPCSession, _pendingPayment.Id);
			pendingPayment.HasBeenPayed.ShouldBe(true);
		}

		[Test]
		public void SubscriptionItems_are_updated()
		{
			var subscriptionItems = GetAll<SubscriptionItem>(GetWPCSession)
				.With(x => x.Id)
				.In(_pendingPayment.SubscriptionItems, x => x.Id);
			subscriptionItems.And(_pendingPayment.SubscriptionItems).Do((freshSubscriptionItem, staleSubscriptionItem) => 
				freshSubscriptionItem.PerformedWithdrawals.ShouldBe(staleSubscriptionItem.PerformedWithdrawals + 1)
			);
		}

		[Test]
		public void Task_creates_a_transaction()
		{
			var transaction = Get<Subscription>(GetWPCSession, _subscription.Id).Transactions.Single();
			transaction.Amount.ShouldBe(_successfulPayment.Amount);
			transaction.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			transaction.Reason.ShouldBe(TransactionReason.Payment);
			transaction.SettlementId.ShouldBe(null);
			transaction.Type.ShouldBe(TransactionType.Deposit);
		}

		[Test]
		public void Task_updates_recieved_payment_as_handled()
		{
			var payment = Get<BGReceivedPayment>(GetBGSession, _successfulPayment.Id);
			payment.Handled.ShouldBe(true);
		}
	}

	[TestFixture, Category("Feature: Receiving Payment")]
	public class When_receiveing_a_failed_payment : TaskBase
	{
		private Task _task;
		ITaskRunnerService _taskRunnerService;
		private int _bankGiroPayerNumber;
		private Subscription _subscription;
		private BGReceivedPayment _failedPayment;

		public When_receiveing_a_failed_payment()
		{
			Context = () =>
			{
				_bankGiroPayerNumber = RegisterPayerWithWebService();
				var shop = CreateShop<Shop>();
				_subscription = StoreSubscription(customer => Factory.CreateSubscription(customer, shop, _bankGiroPayerNumber, SubscriptionConsentStatus.Accepted, new DateTime(2011,01,01)), shop.Id);
				_failedPayment = StoreBGPayment(Factory.CreateFailedPayment, _bankGiroPayerNumber);

				_task = ResolveTask<Task>();
				_taskRunnerService = GetTaskRunnerService(_task);
			};
			Because = () => _taskRunnerService.Run();
		}


		[Test]
		public void Task_does_not_create_a_transaction()
		{
			var fetchedSubscription = Get<Subscription>(GetWPCSession, _subscription.Id);
			fetchedSubscription.Transactions.Count().ShouldBe(0);
		}

		[Test]
		public void Task_creates_a_subscription_error()
		{
			var subscriptionError = GetAll<SubscriptionError>(GetWPCSession).Single();
			subscriptionError.BGPaymentId.ShouldBe(_failedPayment.Id);
			subscriptionError.Code.ShouldBe(null);
			subscriptionError.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			subscriptionError.HandledDate.ShouldBe(null);
			subscriptionError.IsHandled.ShouldBe(false);
			subscriptionError.Subscription.Id.ShouldBe(_subscription.Id);
			subscriptionError.Type.ShouldBe(SubscriptionErrorType.PaymentRejectedInsufficientFunds);
		}

		[Test]
		public void Task_updates_recieved_payment_as_handled()
		{
			var payment = Get<BGReceivedPayment>(GetBGSession, _failedPayment.Id);
			payment.Handled.ShouldBe(true);
		}
	}
}