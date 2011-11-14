using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest.TestHelpers;
using SendConsentTask = Synologen.LensSubscription.ServiceCoordinator.Task.SendConsents.Task;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Sending Consent")]
	public class When_sending_a_consent : TaskBase
	{
		private SendConsentTask task;
		private ITaskRunnerService taskRunnerService;
		private Customer customer;
		private Subscription subscription1, subscription2;
		private int? createdPayerNumber1, createdPayerNumber2;

		public When_sending_a_consent()
		{
			Context = () =>
			{
				var countryToUse = countryRepository.Get(SwedenCountryId);
				var shopToUse = shopRepository.Get(TestShopId);
				customer = Factory.CreateCustomer(countryToUse, shopToUse);
				customerRepository.Save(customer);
				subscription1 = Factory.CreateNewSubscription(customer);
				subscriptionRepository.Save(subscription1);

				subscription2 = Factory.CreateNewSubscription(customer);
				subscriptionRepository.Save(subscription2);

				task = ResolveTask<SendConsentTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};

			Because = () =>
			{
				taskRunnerService.Run();
				var repo = ResolveRepository<ISubscriptionRepository>(GetWPCSession);
				createdPayerNumber1 = repo.Get(subscription1.Id).BankgiroPayerNumber;
				createdPayerNumber2 = repo.Get(subscription2.Id).BankgiroPayerNumber;
			};
		}

		[Test]
		public void Webservice_stores_first_consent()
		{
			var consent = bgConsentRepository.GetAll().Where(x => x.Payer.Id == createdPayerNumber1).First();
			consent.Account.AccountNumber.ShouldBe(subscription1.PaymentInfo.AccountNumber);
			consent.Account.ClearingNumber.ShouldBe(subscription1.PaymentInfo.ClearingNumber);
			consent.OrgNumber.ShouldBe(null);
			consent.Payer.Id.ShouldBe(createdPayerNumber1.Value);
			consent.PersonalIdNumber.ShouldBe(subscription1.Customer.PersonalIdNumber);
			consent.SendDate.ShouldBe(null);
			consent.Type.ShouldBe(ConsentType.New);
		}

		[Test]
		public void Webservice_stores_second_consent()
		{
			var consent = bgConsentRepository.GetAll().Where(x => x.Payer.Id == createdPayerNumber2).First();
			consent.Account.AccountNumber.ShouldBe(subscription2.PaymentInfo.AccountNumber);
			consent.Account.ClearingNumber.ShouldBe(subscription2.PaymentInfo.ClearingNumber);
			consent.OrgNumber.ShouldBe(null);
			consent.Payer.Id.ShouldBe(createdPayerNumber2.Value);
			consent.PersonalIdNumber.ShouldBe(subscription2.Customer.PersonalIdNumber);
			consent.SendDate.ShouldBe(null);
			consent.Type.ShouldBe(ConsentType.New);
		}

		[Test]
		public void Task_updates_consent_as_sent_and_sets_payer_number()
		{
			var fetchedSubscription1 = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription1.Id);
			var fetchedSubscription2 = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription2.Id);
			fetchedSubscription1.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Sent);
			fetchedSubscription2.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Sent);
			fetchedSubscription1.BankgiroPayerNumber.ShouldBe(createdPayerNumber1);
			fetchedSubscription2.BankgiroPayerNumber.ShouldBe(createdPayerNumber2);
		}
	}
}