using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendConsentsTaskTests")]
	public class When_executing_send_consents_task : SendConsentsTaskTestBase
	{
		private IEnumerable<Subscription> expectedSubscriptions;
		private int payerNumber;

		public When_executing_send_consents_task()
		{
			Context = () =>
			{
				payerNumber = 5;
				expectedSubscriptions = SubscriptionFactory.GetListWithAndWithoutBankgiroPayerNumber();
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<AllSubscriptionsToSendConsentsForCriteria>())).Returns(expectedSubscriptions);
				MockedWebServiceClient.Setup(x => x.RegisterPayer(It.IsAny<string>(), AutogiroServiceType.LensSubscription)).Returns(payerNumber);
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
			expectedSubscriptions.Each(subscription => MockedWebServiceClient.Verify(x => x.SendConsent(
					It.Is<ConsentToSend>(consent => consent.PayerNumber.Equals(subscription.BankgiroPayerNumber ?? payerNumber))
			)));
		}

		[Test]
		public void Task_converts_subscriptions_into_consents()
		{
			expectedSubscriptions.Each(subscription => 
				MockedWebServiceClient.Verify(x => x.SendConsent(It.Is<ConsentToSend>(sentConsent => 
					sentConsent.BankAccountNumber.Equals(subscription.PaymentInfo.AccountNumber) && 
					sentConsent.ClearingNumber.Equals(subscription.PaymentInfo.ClearingNumber) &&
					sentConsent.PersonalIdNumber.Equals(subscription.Customer.PersonalIdNumber) &&
					sentConsent.PayerNumber.Equals(subscription.BankgiroPayerNumber ?? payerNumber)
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

		[Test]
		public void Task_registers_new_payers()
		{
			expectedSubscriptions.Where(x => Equals(x.BankgiroPayerNumber, null)).Each(subscription => 
				MockedWebServiceClient.Verify(x => x.RegisterPayer(subscription.Customer.FirstName + " " + subscription.Customer.LastName, AutogiroServiceType.LensSubscription)));
		}

		[Test]
		public void Task_updates_new_subscriptions_with_new_payer_numbers()
		{
			expectedSubscriptions.Where(x => Equals(x.BankgiroPayerNumber, null)).Each( subscription =>
				MockedSubscriptionRepository.Verify(x => x.Save(
					It.Is<Subscription>(savedSubscription => savedSubscription.BankgiroPayerNumber.Value.Equals(payerNumber))
			)));
		}
	}
}