using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendPaymentsTaskTests")]
	public class When_executing_send_payments_task : SendPaymentsTaskTestBase
	{
		private IEnumerable<Subscription> _expectedSubscriptions;
		private DateTime _expectedPaymentDate;
		private int _expectedPaymentReference;

		public When_executing_send_payments_task()
		{
			Context = () =>
			{
				_expectedPaymentDate = new DateTime(2011, 03, 25);
				_expectedSubscriptions = SubscriptionFactory.GetList();
				_expectedPaymentReference = 80;
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<AllSubscriptionsToSendPaymentsForCriteria>())).Returns(_expectedSubscriptions);
				A.CallTo(() => AutogiroPaymentService.GetPaymentDate()).Returns(_expectedPaymentDate);
				A.CallTo(() => SubscriptionPendingPaymentRepository.Save(A<SubscriptionPendingPayment>.Ignored)).Invokes(action => 
					TrySetProperty(action.Arguments[0] as Entity, x => x.Id, _expectedPaymentReference));
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
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_sends_payments_to_webservice()
		{
			_expectedSubscriptions.Each(subscription => MockedWebServiceClient.Verify(x => x.SendPayment(
				It.Is<PaymentToSend>(payment => payment.PayerNumber.Equals(subscription.AutogiroPayerId.Value))
			)));
		}

		[Test]
		public void Task_converts_transactions_into_payments()
		{
			_expectedSubscriptions.Each(subscription =>
               MockedWebServiceClient.Verify(x => x.SendPayment(It.Is<PaymentToSend>(sentPayment =>
                 sentPayment.Amount.Equals(subscription.SubscriptionItems.Sum(y => y.MonthlyWithdrawalAmount)) &&
                 sentPayment.PayerNumber.Equals(subscription.AutogiroPayerId) &&
                 Equals(sentPayment.Reference, _expectedPaymentReference.ToString()) &&
                 sentPayment.Type.Equals(PaymentType.Debit) &&
                 sentPayment.PeriodCode.Equals(PaymentPeriodCode.PaymentOnceOnSelectedDate) &&
				 sentPayment.PaymentDate.Equals(_expectedPaymentDate)
        	))));
}

		[Test]
		public void Task_updates_subscription_payment_date()
		{
			_expectedSubscriptions.Each(subscription =>
				MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(savedSubscription =>
				   savedSubscription.Id.Equals(subscription.Id) &&
				   //savedSubscription.PaymentInfo.PaymentSentDate.Equals(savedSubscription.PaymentInfo.PaymentSentDate) // <-- Test?
				   savedSubscription.LastPaymentSent.Value.Equals(_expectedPaymentDate)
			))));
		}
	}
}