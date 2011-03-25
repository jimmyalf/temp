using System;
using System.Collections.Generic;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendPaymentsTaskTests")]
	public class When_executing_send_payments_task : SendPaymentsTaskTestBase
	{
		
		private IEnumerable<Subscription> expectedSubscriptions;
		private DateTime expectedPaymentDate;

		public When_executing_send_payments_task()
		{
			Context = () =>
			{
				expectedPaymentDate = new DateTime(2011, 03, 25);
				expectedSubscriptions = SubscriptionFactory.GetList();
				MockedSubscriptionRepository
					.Setup(x => x.FindBy(It.IsAny<AllSubscriptionsToSendPaymentsForCriteria>()))
					.Returns(expectedSubscriptions);
				A.CallTo(() => AutogiroPaymentService.GetPaymentDate()).Returns(expectedPaymentDate);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_fetches_expected_transactions_from_repository()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.IsAny<AllSubscriptionsToSendPaymentsForCriteria>()));
		}

		[Test]
		public void Task_loggs_start_and_stop_info_messages()
		{
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Started"))), Times.Once());
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Finished"))), Times.Once());
		}

		[Test]
		public void Task_sends_payments_to_webservice()
		{
			expectedSubscriptions.Each(subscription => MockedWebServiceClient.Verify(x => x.SendPayment(
				It.Is<PaymentToSend>(payment => payment.PayerNumber.Equals(subscription.BankgiroPayerNumber.Value))
			)));
		}

		[Test]
		public void Task_converts_transactions_into_payments()
		{
			expectedSubscriptions.Each(subscription =>
               MockedWebServiceClient.Verify(x => x.SendPayment(It.Is<PaymentToSend>(sentPayment =>
                 sentPayment.Amount.Equals(subscription.PaymentInfo.MonthlyAmount) &&
                 sentPayment.PayerNumber.Equals(subscription.BankgiroPayerNumber) &&
                 Equals(sentPayment.Reference, null) &&
                 sentPayment.Type.Equals(PaymentType.Debit) &&
                 sentPayment.PeriodCode.Equals(PaymentPeriodCode.PaymentOnceOnSelectedDate) &&
				 sentPayment.PaymentDate.Equals(expectedPaymentDate)
        	))));
}

		[Test]
		public void Task_updates_subscription_payment_date()
		{
			expectedSubscriptions.Each(subscription =>
				MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(savedSubscription =>
				   savedSubscription.Id.Equals(subscription.Id) &&
				   //savedSubscription.PaymentInfo.PaymentSentDate.Equals(savedSubscription.PaymentInfo.PaymentSentDate) // <-- Test?
				   savedSubscription.PaymentInfo.PaymentSentDate.Value.Equals(expectedPaymentDate)
			))));
		}
	}
}