using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("ReceivePaymentsTaskTests")]
	public class When_executing_receive_payments_task : ReceivePaymentsTaskTestBase
	{
		private IEnumerable<ReceivedPayment> _expectedPayments;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;
		private readonly Func<ReceivedPayment, bool> _isErrorPayment = payment => (payment.Result == PaymentResult.AGConnectionMissing || payment.Result == PaymentResult.InsufficientFunds);
		private readonly Func<ReceivedPayment, bool> _isTransactionPayment = payment => (payment.Result == PaymentResult.WillTryAgain || payment.Result == PaymentResult.Approved);
		private IEnumerable<ReceivedPayment> _expectedErrorPayments;
		private IEnumerable<ReceivedPayment> _expectedTransactionPayments;

		public When_executing_receive_payments_task()
		{
			Context = () =>
			{
				_expectedPayments = PaymentFactory.GetList(SubscriptionId).ToList();
				_expectedErrorPayments = _expectedPayments.Where(_isErrorPayment);
				_expectedTransactionPayments = _expectedPayments.Where(_isTransactionPayment);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);
				var pendingPayments = PendingPaymentFactory.GetList(_expectedPayments);

				MockedWebServiceClient.Setup(x => x.GetPayments(AutogiroServiceType.SubscriptionVersion2)).Returns(_expectedPayments.ToArray());
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
				foreach (var receivedPayment in _expectedPayments)
				{
					var referenceId = Int32.Parse(receivedPayment.Reference);
					A.CallTo(() => SubscriptionPendingPaymentRepository.Get(referenceId)).Returns(pendingPayments.Single(x => x.Id == referenceId));
				}
				
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_logs_start_and_stop_info_messages()
		{
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_logs_number_of_received_payments()
		{
			LoggingService.AssertDebug("Fetched 16 payment results from bgc server");
		}

		[Test]
		public void Task_logs_after_each_handled_payment()
		{
			LoggingService.AssertDebug(messages => messages.Count(m => m.Contains("rejected")) == 8);
			LoggingService.AssertDebug(messages => messages.Count(m => m.Contains("was accepted")) == 4);
			LoggingService.AssertDebug(messages => messages.Count(m => m.Contains("failed")) == 4);
		}

		[Test]
		public void Task_fetches_matching_subscriptions_from_repository()
		{
			_expectedPayments.Each(recievedPayment =>
				MockedSubscriptionRepository.Verify(x =>
					x.GetByBankgiroPayerId(It.Is<int>(id => id.Equals(recievedPayment.PayerNumber))
			)));
		}

		[Test]
		public void Task_saves_transactions_to_repository()
		{
			_expectedTransactionPayments.Each(payment => MockedTransactionRepository.Verify(x => x.Save(
				It.Is<SubscriptionTransaction>(savedTransaction =>
					Equals(savedTransaction.Amount, payment.Amount) &&
					//Equals(savedTransaction.Article, null) &&
					Equals(savedTransaction.CreatedDate.Date, DateTime.Now.Date) &&
					MatchReason(savedTransaction.Reason, payment.Result) &&
					Equals(savedTransaction.SettlementId, null) &&
					Equals(savedTransaction.Subscription.Id, _expectedSubscription.Id) &&
					MatchTransactionType(savedTransaction.Type, payment.Type)
			))));
		}

		[Test]
		public void Task_saves_subscriptionerrors_to_repository()
		{
			_expectedErrorPayments.Each(payment => MockedSubscriptionErrorRepository.Verify(x => x.Save(
               	It.Is<SubscriptionError>(savedError => 
					Equals(savedError.BGErrorId, null) &&
					Equals(savedError.BGPaymentId, payment.PaymentId) &&
					Equals(savedError.Code, null) &&
					Equals(savedError.CreatedDate.Date, DateTime.Now.Date) &&
					Equals(savedError.HandledDate, null) &&
					Equals(savedError.IsHandled, false) &&
					Equals(savedError.Subscription.Id, _expectedSubscription.Id) &&
					MatchErrorType(savedError.Type, payment.Result)
			))));
		}

		[Test]
		public void Task_sets_payment_as_handled_to_webservice()
		{
			_expectedPayments.Each(payment => 
				MockedWebServiceClient.Verify(x => x.SetPaymentHandled(
						It.Is<ReceivedPayment>(paymentItem => paymentItem.PaymentId.Equals(payment.PaymentId))
			)));
		}
	}

	[TestFixture, Category("ReceivePaymentsTaskTests")]
	public class When_receiveing_approved_payment : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment _expectedPayment;
		private Subscription _expectedSubscription;
		private int _subscriptionId = 3;
		private SubscriptionPendingPayment _pendingPayment;

		public When_receiveing_approved_payment()
		{
			Context = () =>
			{
				const decimal taxedAmount = 250.25M;
				const int taxFreeAmount = 100;
				_expectedPayment = PaymentFactory.Get(subscriptionId:_subscriptionId, amount: taxedAmount + taxFreeAmount);
				_expectedSubscription = SubscriptionFactory.Get(_subscriptionId);
				_pendingPayment = PendingPaymentFactory.Get(taxedAmount, taxFreeAmount);
				MockedWebServiceClient.Setup(x => x.GetPayments(AutogiroServiceType.SubscriptionVersion2)).Returns(new []{_expectedPayment});
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
				A.CallTo(() => SubscriptionPendingPaymentRepository.Get(Int32.Parse(_expectedPayment.Reference))).Returns(_pendingPayment);

			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Transaction_is_saved_with_expected_values()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Amount.Equals(_expectedPayment.Amount))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Year.Equals(DateTime.Now.Year))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Month.Equals(DateTime.Now.Month))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Minute.Equals(DateTime.Now.Minute))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Reason.Equals(TransactionReason.Payment))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Subscription.Id.Equals(_subscriptionId))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Type.Equals(TransactionType.Deposit))));
		}

		[Test]
		public void No_subscriptionerror_is_saved()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.IsAny<SubscriptionError>()), Times.Never());
		}
	}

	[TestFixture, Category("ReceivePaymentsTaskTests")]
	public class When_receiveing_rejected_payment_because_of_insufficientfunds : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment _expectedPayment;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiveing_rejected_payment_because_of_insufficientfunds()
		{
			Context = () =>
			{
				_expectedPayment = PaymentFactory.Get(SubscriptionId, result: PaymentResult.InsufficientFunds);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments(AutogiroServiceType.SubscriptionVersion2)).Returns(new []{ _expectedPayment });
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void subscriptionerror_is_saved_with_expected_values()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Year.Equals(DateTime.Now.Year))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Month.Equals(DateTime.Now.Month))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Minute.Equals(DateTime.Now.Minute))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.PaymentRejectedInsufficientFunds))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));

		}

		[Test]
		public void No_transaction_is_saved()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.IsAny<SubscriptionTransaction>()), Times.Never());
		}
	}

	[TestFixture, Category("ReceivePaymentsTaskTests")]
	public class When_receiveing_rejected_payment_because_of_agconnectionmissing : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment _expectedPayment;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiveing_rejected_payment_because_of_agconnectionmissing()
		{
			Context = () =>
			{
				_expectedPayment = PaymentFactory.Get(SubscriptionId, result: PaymentResult.AGConnectionMissing);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments(AutogiroServiceType.SubscriptionVersion2)).Returns(new[] {_expectedPayment});
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void subscriptionerror_is_saved_with_expected_values()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Code.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Year.Equals(DateTime.Now.Year))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Month.Equals(DateTime.Now.Month))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.CreatedDate.Minute.Equals(DateTime.Now.Minute))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.HandledDate.Equals(null))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.IsHandled.Equals(false))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Type.Equals(SubscriptionErrorType.PaymentRejectedAgConnectionMissing))));
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(SubscriptionId))));

		}

		[Test]
		public void No_transaction_is_saved()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.IsAny<SubscriptionTransaction>()), Times.Never());
		}
	}

	[TestFixture, Category("ReceivePaymentsTaskTests")]
	public class When_receiveing_failed_payment : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment _expectedPayment;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiveing_failed_payment()
		{
			Context = () =>
			{
				_expectedPayment = PaymentFactory.Get(SubscriptionId, result: PaymentResult.WillTryAgain);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments(AutogiroServiceType.SubscriptionVersion2)).Returns(new[] {_expectedPayment});
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Transaction_is_saved_with_expected_values()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Amount.Equals(_expectedPayment.Amount))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Year.Equals(DateTime.Now.Year))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Month.Equals(DateTime.Now.Month))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Minute.Equals(DateTime.Now.Minute))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Reason.Equals(TransactionReason.PaymentFailed))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Subscription.Id.Equals(SubscriptionId))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Type.Equals(TransactionType.Deposit))));
		}

		[Test]
		public void No_subscriptionerror_is_saved()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.IsAny<SubscriptionError>()), Times.Never());
		}
	}

	[TestFixture, Category("ReceivePaymentsTaskTests")]
	public class When_receiving_payments_gets_exception_from_web_service : ReceivePaymentsTaskTestBase
	{
		private IEnumerable<ReceivedPayment> _expectedPayments;
		private Subscription _expectedSubscription;
		private const int SubscriptionId = 1;

		public When_receiving_payments_gets_exception_from_web_service()
		{
			Context = () =>
			{
				_expectedPayments = PaymentFactory.GetList(SubscriptionId);
				_expectedSubscription = SubscriptionFactory.Get(SubscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments(It.IsAny<AutogiroServiceType>())).Returns(_expectedPayments.ToArray());
				MockedWebServiceClient.Setup(x => x.SetPaymentHandled(It.IsAny<ReceivedPayment>())).Throws(new Exception("Test exception"));
				MockedWebServiceClient.SetupGet(x => x.State).Returns(CommunicationState.Faulted);
				MockedSubscriptionRepository.Setup(x => x.GetByBankgiroPayerId(It.IsAny<int>())).Returns(_expectedSubscription);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_logs_error_for_each_exception()
		{
			Assert.Inconclusive("TODO");
			//MockedLogger.Verify(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.Exactly(expectedPayments.Count()));
		}

		[Test]
		public void Task_fetches_new_webclient_for_each_exception()
		{
		    A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGWebServiceClient>()).MustHaveHappened(Repeated.Exactly.Times(_expectedPayments.Count()));
		}
	}
}