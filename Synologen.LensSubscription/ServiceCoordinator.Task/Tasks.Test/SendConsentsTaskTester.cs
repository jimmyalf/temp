using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendConsentsTaskTests")]
	public class When_executing_send_consents_task : SendConsentsTaskTestBase
	{
		private IEnumerable<Subscription> _expectedSubscriptions;
		private int _payerNumber;

		public When_executing_send_consents_task()
		{
			Context = () =>
			{
				_payerNumber = 5;
				_expectedSubscriptions = SubscriptionFactory.GetListWithAndWithoutBankgiroPayerNumber();
				MockedSubscriptionRepository.Setup(x => x.FindBy(It.IsAny<AllSubscriptionsToSendConsentsForCriteria>())).Returns(_expectedSubscriptions);
				MockedWebServiceClient.Setup(x => x.RegisterPayer(It.IsAny<string>(), AutogiroServiceType.SubscriptionVersion2)).Returns(_payerNumber);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_fetches_expected_consents_from_repository()
		{
			MockedSubscriptionRepository.Verify(x => x.FindBy(It.IsAny<AllSubscriptionsToSendConsentsForCriteria>()));
		}

		[Test]
		public void Task_loggs_start_and_stop_info_messages()
		{
			LoggingService.AssertInfo("Started");
			LoggingService.AssertInfo("Finished");
		}

		[Test]
		public void Task_sends_consents_to_webservice()
		{
			_expectedSubscriptions.Each(subscription => MockedWebServiceClient.Verify(x => x.SendConsent(
					It.Is<ConsentToSend>(consent => consent.PayerNumber.Equals(subscription.AutogiroPayerId ?? _payerNumber))
			)));
		}

		[Test]
		public void Task_converts_subscriptions_into_consents()
		{
			_expectedSubscriptions.Each(subscription => 
				MockedWebServiceClient.Verify(x => x.SendConsent(It.Is<ConsentToSend>(sentConsent => 
					sentConsent.BankAccountNumber.Equals(subscription.BankAccountNumber) && 
					sentConsent.ClearingNumber.Equals(subscription.ClearingNumber) &&
					sentConsent.PersonalIdNumber.Equals(subscription.Customer.PersonalIdNumber) &&
					sentConsent.PayerNumber.Equals(subscription.AutogiroPayerId ?? _payerNumber)
			))));
		}

		[Test]
		public void Task_updates_sent_consents_to_repository()
		{
			_expectedSubscriptions.Each(subscription => 
				MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(savedSubscription => 
					savedSubscription.Id.Equals(subscription.Id) &&
					savedSubscription.ConsentStatus.Equals(SubscriptionConsentStatus.Sent)
			))));
		}

		[Test]
		public void Task_registers_new_payers()
		{
			_expectedSubscriptions.Where(x => Equals(x.AutogiroPayerId, null)).Each(subscription => 
				MockedWebServiceClient.Verify(x => x.RegisterPayer(subscription.Customer.FirstName + " " + subscription.Customer.LastName, AutogiroServiceType.SubscriptionVersion2)));
		}

		[Test]
		public void Task_updates_new_subscriptions_with_new_payer_numbers()
		{
			_expectedSubscriptions.Where(x => Equals(x.AutogiroPayerId, null)).Each( subscription =>
				MockedSubscriptionRepository.Verify(x => x.Save(
					It.Is<Subscription>(savedSubscription => savedSubscription.AutogiroPayerId.Value.Equals(_payerNumber))
			)));
		}
	}
}