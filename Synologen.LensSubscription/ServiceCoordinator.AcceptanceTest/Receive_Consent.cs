using System;
using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest.TestHelpers;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGData.Repositories;
using ReceiveConsentsTask = Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveConsents.Task;

namespace ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Payment")]
	public class When_receiveing_a_consented_consent : TaskBase
	{
		private ReceiveConsentsTask task;
		private ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Customer customer;
		private Subscription subscription;
		private BGReceivedConsent consentedConsent;

		public When_receiveing_a_consented_consent()
		{
			Context = () =>
			{

				InvokeWebService(service =>
				{
					bankGiroPayerNumber = service.RegisterPayer("Test payer", AutogiroServiceType.LensSubscription);
				});

				var autogiroPayer = autogiroPayerRepository.Get(bankGiroPayerNumber);
				var countryToUse = countryRepository.Get(SwedenCountryId);
				var shopToUse = shopRepository.Get(TestShopId);
				customer = Factory.CreateCustomer(countryToUse, shopToUse);
				customerRepository.Save(customer);
				subscription = Factory.CreateSentSubscription(customer, bankGiroPayerNumber);
				subscriptionRepository.Save(subscription);

				consentedConsent = Factory.CreateConsentedConsent(autogiroPayer);
				bgReceivedConsentRepository.Save(consentedConsent);

				task = ResolveTask<ReceiveConsentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Task_updates_subscription_as_consented()
		{
			var fetchedSubscription = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription.Id);
			fetchedSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Accepted);
			fetchedSubscription.ActivatedDate.ShouldBe(consentedConsent.ConsentValidForDate);
		}

		[Test]
		public void Task_updates_recieved_consent_as_handled()
		{
			var consent = new BGReceivedConsentRepository(GetBGSession()).Get(consentedConsent.Id);
			consent.Handled.ShouldBe(true);
		}
	}

	[TestFixture, Category("Feature: Receiving Payment")]
	public class When_receiveing_a_failed_consent : TaskBase
	{
		private ReceiveConsentsTask task;
		private ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Customer customer;
		private Subscription subscription;
		private BGReceivedConsent failedConsent;

		public When_receiveing_a_failed_consent()
		{
			Context = () =>
			{

				InvokeWebService(service =>
				{
					bankGiroPayerNumber = service.RegisterPayer("Test payer", AutogiroServiceType.LensSubscription);
				});

				var autogiroPayer = autogiroPayerRepository.Get(bankGiroPayerNumber);
				var countryToUse = countryRepository.Get(SwedenCountryId);
				var shopToUse = shopRepository.Get(TestShopId);
				customer = Factory.CreateCustomer(countryToUse, shopToUse);
				customerRepository.Save(customer);
				subscription = Factory.CreateSentSubscription(customer, bankGiroPayerNumber);
				subscriptionRepository.Save(subscription);

				failedConsent = Factory.CreateFailedConsent(autogiroPayer);
				bgReceivedConsentRepository.Save(failedConsent);

				task = ResolveTask<ReceiveConsentsTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Task_updates_subscription_as_consent_denied()
		{
			var fetchedSubscription = ResolveRepository<ISubscriptionRepository>(GetWPCSession).Get(subscription.Id);
			fetchedSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Denied);
		}

		[Test]
		public void Task_creates_a_subscription_error()
		{
			var lastError = subscriptionErrorRepository.GetAll().Last();
			lastError.BGConsentId.ShouldBe(failedConsent.Id);
			lastError.Code.ShouldBe(failedConsent.InformationCode);
			lastError.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			lastError.HandledDate.ShouldBe(null);
			lastError.IsHandled.ShouldBe(false);
			lastError.Subscription.Id.ShouldBe(subscription.Id);
			lastError.Type.ShouldBe(SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber);
		}

		[Test]
		public void Task_updates_recieved_consent_as_handled()
		{
			var consent = new BGReceivedConsentRepository(GetBGSession()).Get(failedConsent.Id);
			consent.Handled.ShouldBe(true);
		}
	}
}