using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Synologen.ServiceCoordinator.Test.Factories;
using Synologen.ServiceCoordinator.Test.TestHelpers;

namespace Synologen.ServiceCoordinator.Test
{
	[TestFixture]
	public class When_executing_send_payments_task : SendPaymentsTaskTestBase
	{
		
		private IEnumerable<Subscription> expectedSubscriptions;

		public When_executing_send_payments_task()
		{
			Context = () =>
			{
				expectedSubscriptions = SubscriptionFactory.GetList();
				MockedSubscriptionRepository
					.Setup(x => x.FindBy(It.IsAny<AllSubscriptionsToSendPaymentsForCriteria>()))
					.Returns(expectedSubscriptions);
			};
			Because = task => task.Execute();
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
			MockedWebServiceClient.Verify(
				x => x.SendPayment(It.IsAny<PaymentToSend>()),
				Times.Exactly(expectedSubscriptions.Count())
			);
		}

		[Test]
		public void Task_converts_transactions_into_payments()
		{
			expectedSubscriptions.Each(subscription =>
				MockedWebServiceClient.Verify(x => x.SendPayment(It.Is<PaymentToSend>(sentPayment =>
					sentPayment.Amount.Equals(subscription.PaymentInfo.MonthlyAmount) &&
					sentPayment.PayerId.Equals(subscription.Id) &&
					sentPayment.Reference.Equals(subscription.Customer.PersonalIdNumber) &&
					sentPayment.Type.Equals(PaymentType.Debit)
			))));
		}

		[Test]
		public void Task_updates_sent_payments_to_repository()
		{
			expectedSubscriptions.Each(subscription =>
				MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(savedSubscription =>
					savedSubscription.Id.Equals(subscription.Id) &&
					savedSubscription.PaymentInfo.PaymentSentDate.Equals(savedSubscription.PaymentInfo.PaymentSentDate)
			))));
		}
	}
}
