using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture]
	public class When_executing_receive_payments_task : ReceivePaymentsTaskTestBase
	{
		private IEnumerable<ReceivedPayment> expectedPayments;
		private Subscription expectedSubscription;
		private const int subscriptionId = 1;

		public When_executing_receive_payments_task()
		{
			Context = () =>
			{
				expectedPayments = PaymentFactory.GetList(subscriptionId);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments()).Returns(expectedPayments.ToArray);
				MockedSubscriptionRepository
					.Setup(x => x.Get(It.IsAny<int>()))
					.Returns(expectedSubscription);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Task_logs_start_and_stop_info_messages()
		{
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Started"))), Times.Once());
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Finished"))), Times.Once());
		}

		[Test]
		public void Task_logs_number_of_received_payments()
		{
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("Fetched 16 payment results from bgc server"))), Times.Once());
		}

		[Test]
		public void Task_logs_after_each_handled_payment()
		{
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("rejected"))), Times.Exactly(8));
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("was accepted"))), Times.Exactly(4));
			MockedLogger.Verify(x => x.Debug(It.Is<string>(message => message.Contains("failed"))), Times.Exactly(4));
		}

		[Test]
		public void Task_fetches_matching_subscriptions_from_repository()
		{
			expectedPayments.Each(recievedPayment =>
			                      MockedSubscriptionRepository.Verify(x =>
			                                                          x.Get(It.Is<int>(id => id.Equals(recievedPayment.PayerId))
			                                                          	)));
		}

		[Test]
		public void Task_saves_transactions_to_repository()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.IsAny<SubscriptionTransaction>()), Times.Exactly(8));
		}

		[Test]
		public void Task_saves_subscriptionerrors_to_repository()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.IsAny<SubscriptionError>()), Times.Exactly(8));
		}
	}

	[TestFixture]
	public class When_receiveing_approved_payment : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment[] expectedPayments;
		private Subscription expectedSubscription;
		private const int subscriptionId = 1;

		public When_receiveing_approved_payment()
		{
			Context = () =>
			{
				expectedPayments = PaymentFactory.Get(subscriptionId, PaymentResult.Approved);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments()).Returns(expectedPayments.ToArray);
				MockedSubscriptionRepository
					.Setup(x => x.Get(It.IsAny<int>()))
					.Returns(expectedSubscription);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Transaction_is_saved_with_expected_values()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Amount.Equals(expectedPayments[0].Amount))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Year.Equals(DateTime.Now.Year))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Month.Equals(DateTime.Now.Month))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Minute.Equals(DateTime.Now.Minute))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Reason.Equals(TransactionReason.Payment))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Subscription.Id.Equals(subscriptionId))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Type.Equals(TransactionType.Deposit))));
		}

		[Test]
		public void No_subscriptionerror_is_saved()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.IsAny<SubscriptionError>()), Times.Never());
		}
	}

	[TestFixture]
	public class When_receiveing_rejected_payment_because_of_insufficientfunds : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment[] expectedPayments;
		private Subscription expectedSubscription;
		private const int subscriptionId = 1;

		public When_receiveing_rejected_payment_because_of_insufficientfunds()
		{
			Context = () =>
			{
				expectedPayments = PaymentFactory.Get(subscriptionId, PaymentResult.InsufficientFunds);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments()).Returns(expectedPayments.ToArray);
				MockedSubscriptionRepository
					.Setup(x => x.Get(It.IsAny<int>()))
					.Returns(expectedSubscription);
			};
			Because = task => task.Execute();
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));

		}

		[Test]
		public void No_transaction_is_saved()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.IsAny<SubscriptionTransaction>()), Times.Never());
		}
	}

	[TestFixture]
	public class When_receiveing_rejected_payment_because_of_agconnectionmissing : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment[] expectedPayments;
		private Subscription expectedSubscription;
		private const int subscriptionId = 1;

		public When_receiveing_rejected_payment_because_of_agconnectionmissing()
		{
			Context = () =>
			{
				expectedPayments = PaymentFactory.Get(subscriptionId, PaymentResult.AGConnectionMissing);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments()).Returns(expectedPayments.ToArray);
				MockedSubscriptionRepository
					.Setup(x => x.Get(It.IsAny<int>()))
					.Returns(expectedSubscription);
			};
			Because = task => task.Execute();
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
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.Is<SubscriptionError>(subscriptionError => subscriptionError.Subscription.Id.Equals(subscriptionId))));

		}

		[Test]
		public void No_transaction_is_saved()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.IsAny<SubscriptionTransaction>()), Times.Never());
		}
	}

	[TestFixture]
	public class When_receiveing_failed_payment : ReceivePaymentsTaskTestBase
	{
		private ReceivedPayment[] expectedPayments;
		private Subscription expectedSubscription;
		private const int subscriptionId = 1;

		public When_receiveing_failed_payment()
		{
			Context = () =>
			{
				expectedPayments = PaymentFactory.Get(subscriptionId, PaymentResult.WillTryAgain);
				expectedSubscription = SubscriptionFactory.Get(subscriptionId);

				MockedWebServiceClient.Setup(x => x.GetPayments()).Returns(expectedPayments.ToArray);
				MockedSubscriptionRepository
					.Setup(x => x.Get(It.IsAny<int>()))
					.Returns(expectedSubscription);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Transaction_is_saved_with_expected_values()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Amount.Equals(expectedPayments[0].Amount))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Year.Equals(DateTime.Now.Year))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Month.Equals(DateTime.Now.Month))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.CreatedDate.Minute.Equals(DateTime.Now.Minute))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Reason.Equals(TransactionReason.PaymentFailed))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Subscription.Id.Equals(subscriptionId))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(transaction => transaction.Type.Equals(TransactionType.Deposit))));
		}

		[Test]
		public void No_subscriptionerror_is_saved()
		{
			MockedSubscriptionErrorRepository.Verify(x => x.Save(It.IsAny<SubscriptionError>()), Times.Never());
		}
	}
}