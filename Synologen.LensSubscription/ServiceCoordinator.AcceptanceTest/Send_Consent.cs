using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest.TestHelpers;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using SendConsentTask = Synologen.LensSubscription.ServiceCoordinator.Task.SendConsents.Task;

namespace ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Sending Consent")]
	public class When_sending_a_consent : TaskBase
	{
		private SendConsentTask task;
		private ITaskRunnerService taskRunnerService;
		private Customer customer;
		private Subscription subscription;
		private int? createdPayerNumber;

		public When_sending_a_consent()
		{
			Context = () =>
			{
				var countryToUse = countryRepository.Get(SwedenCountryId);
				var shopToUse = shopRepository.Get(TestShopId);
				customer = Factory.CreateCustomer(countryToUse, shopToUse);
				customerRepository.Save(customer);
				subscription = Factory.CreateNewSubscription(customer);
				subscriptionRepository.Save(subscription);

				task = ResolveTask<SendConsentTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};

			Because = () =>
			{
				taskRunnerService.Run();
				createdPayerNumber = ResolveRepository<ISubscriptionRepository>(GetWPCSession)
					.Get(subscription.Id)
					.BankgiroPayerNumber;
			};
		}

		[Test]
		public void Webservice_stores_consent()
		{
			var lastConsent = bgConsentRepository.GetAll().Last();
			lastConsent.Account.AccountNumber.ShouldBe(subscription.PaymentInfo.AccountNumber);
			lastConsent.Account.ClearingNumber.ShouldBe(subscription.PaymentInfo.ClearingNumber);
			lastConsent.OrgNumber.ShouldBe(null);
			lastConsent.Payer.Id.ShouldBe(createdPayerNumber.Value);
			lastConsent.PersonalIdNumber.ShouldBe(subscription.Customer.PersonalIdNumber);
			lastConsent.SendDate.ShouldBe(null);
			lastConsent.Type.ShouldBe(ConsentType.New);
		}

		[Test]
		public void Task_updates_consent_as_sent_and_sets_payer_number()
		{
			var fetchedSubscription = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription.Id);
			fetchedSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Sent);
			fetchedSubscription.BankgiroPayerNumber.ShouldBe(createdPayerNumber);
		}
	}
}
