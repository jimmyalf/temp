using System;
using System.Linq;
using NUnit.Framework;
using ServiceCoordinator.AcceptanceTest;
using ServiceCoordinator.AcceptanceTest.TestHelpers;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGData.Repositories;
using ReceiveConsentsTask = Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveConsents.Task;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Consent")]
	public class When_receiveing_a_successful_consent : ReceiveConsentTaskbase
	{
		private ReceiveConsentsTask task;
		private ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Subscription subscription;
		private BGReceivedConsent consentedConsent;

		public When_receiveing_a_successful_consent()
		{
			Context = () =>
			{
				bankGiroPayerNumber = RegisterPayerWithWebService();
				subscription = StoreSubscription(customer => Factory.CreateSentSubscription(customer, bankGiroPayerNumber), bankGiroPayerNumber);
				consentedConsent = StoreBGConsent(Factory.CreateConsentedConsent, bankGiroPayerNumber);

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

	[TestFixture, Category("Feature: Receiving Consent")]
	public class When_receiveing_a_failed_consent : ReceiveConsentTaskbase
	{
		private ReceiveConsentsTask task;
		private ITaskRunnerService taskRunnerService;
		private int bankGiroPayerNumber;
		private Subscription subscription;
		private BGReceivedConsent failedConsent;

		public When_receiveing_a_failed_consent()
		{
			Context = () =>
			{
				bankGiroPayerNumber = RegisterPayerWithWebService();
				subscription = StoreSubscription(customer => Factory.CreateSentSubscription(customer, bankGiroPayerNumber), bankGiroPayerNumber);
				failedConsent = StoreBGConsent(Factory.CreateFailedConsent, bankGiroPayerNumber);

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