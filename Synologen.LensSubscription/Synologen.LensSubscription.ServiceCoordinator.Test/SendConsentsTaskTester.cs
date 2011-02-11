using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Synologen.LensSubscription.ServiceCoordinator.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Test
{
	[TestFixture]
	public class When_executing_send_consents_task : SendConsentsTaskTestBase
	{
		private IEnumerable<Subscription> expectedSubscriptions;

		public When_executing_send_consents_task()
		{
			Context = () =>
			{
				expectedSubscriptions = SubscriptionFactory.GetList();
				MockedSubscriptionRepository
					.Setup(x => x.FindBy(It.IsAny<AllSubscriptionsToSendConsentsForCriteria>()))
					.Returns(expectedSubscriptions);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Task_fetches_expected_consents_from_repository()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.IsAny<AllSubscriptionsToSendConsentsForCriteria>()));
		}

		[Test]
		public void Task_loggs_start_and_stop_info_messages()
		{
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Started"))), Times.Once());
			MockedLogger.Verify(x => x.Info(It.Is<string>(message => message.Contains("Finished"))), Times.Once());
		}

		[Test]
		public void Task_sends_consents_to_webservice()
		{
			MockedWebServiceClient.Verify(
				x => x.SendConsent(It.IsAny<ConsentToSend>()), 
				Times.Exactly(expectedSubscriptions.Count())
				);
		}

		[Test]
		public void Task_converts_subscriptions_into_consents()
		{
			expectedSubscriptions.Each(subscription => 
				MockedWebServiceClient.Verify(x => x.SendConsent(It.Is<ConsentToSend>(sentConsent => 
					sentConsent.BankAccountNumber.Equals(subscription.PaymentInfo.AccountNumber) && 
					sentConsent.ClearingNumber.Equals(subscription.PaymentInfo.ClearingNumber) &&
					sentConsent.PersonalIdNumber.Equals(subscription.Customer.PersonalIdNumber) &&
					sentConsent.PayerId.Equals(subscription.Id)
			))));
		}

		[Test]
		public void Task_updates_sent_consents_to_repository()
		{
			expectedSubscriptions.Each(subscription => 
				MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(savedSubscription => 
					savedSubscription.Id.Equals(subscription.Id) &&
					savedSubscription.ConsentStatus.Equals(SubscriptionConsentStatus.Sent)
			))));
		}
	}
}