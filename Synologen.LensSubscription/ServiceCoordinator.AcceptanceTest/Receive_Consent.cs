using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest.TestHelpers;
using ReceiveConsentsTask = Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveConsents.Task;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Receiving Consent")]
	public class When_receiveing_a_successful_consent : TaskBase
	{
		private ReceiveConsentsTask _task;
		private ITaskRunnerService _taskRunnerService;
		private int _bankGiroPayerNumber;
		private BGReceivedConsent _consentedConsent;
		private Subscription _subscription;

		public When_receiveing_a_successful_consent()
		{
			Context = () =>
			{
				_bankGiroPayerNumber = RegisterPayerWithWebService();
				var shop = CreateShop<Shop>();
				_subscription = StoreSubscription(customer => Factory.CreateSubscription(customer, shop, _bankGiroPayerNumber, SubscriptionConsentStatus.Sent, new DateTime(2011,01,01)), shop.Id);
				_consentedConsent = StoreBGConsent(Factory.CreateConsentedConsent, _bankGiroPayerNumber);

				_task = ResolveTask<ReceiveConsentsTask>();
				_taskRunnerService = GetTaskRunnerService(_task);
			};
			Because = () => _taskRunnerService.Run();
		}

		[Test]
		public void Task_updates_subscription_as_consented()
		{
			var fetchedSubscription = GetWPCSession().Get<Subscription>(_subscription.Id);
			fetchedSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Accepted);
			fetchedSubscription.ActivatedDate.ShouldBe(_consentedConsent.ConsentValidForDate);
		}

		[Test]
		public void Task_updates_recieved_consent_as_handled()
		{
			var consent = new BGReceivedConsentRepository(GetBGSession()).Get(_consentedConsent.Id);
			consent.Handled.ShouldBe(true);
		}
	}

	[TestFixture, Category("Feature: Receiving Consent")]
	public class When_receiveing_a_failed_consent : TaskBase
	{
		private ReceiveConsentsTask _task;
		private ITaskRunnerService _taskRunnerService;
		private int _bankGiroPayerNumber;
		private Subscription _subscription;
		private BGReceivedConsent _failedConsent;

		public When_receiveing_a_failed_consent()
		{
			Context = () =>
			{
				_bankGiroPayerNumber = RegisterPayerWithWebService();
				var shop = CreateShop<Shop>();
				_subscription = StoreSubscription(customer => Factory.CreateSubscription(customer, shop, _bankGiroPayerNumber, SubscriptionConsentStatus.Sent, new DateTime(2011,01,01)), shop.Id);
				_failedConsent = StoreBGConsent(Factory.CreateFailedConsent, _bankGiroPayerNumber);

				_task = ResolveTask<ReceiveConsentsTask>();
				_taskRunnerService = GetTaskRunnerService(_task);
			};
			Because = () => _taskRunnerService.Run();
		}

		[Test]
		public void Task_updates_subscription_as_consent_denied()
		{
			var fetchedSubscription = Get<Subscription>(GetWPCSession, _subscription.Id);
			fetchedSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Denied);
		}

		[Test]
		public void Task_creates_a_subscription_error()
		{
			var lastError = GetAll<SubscriptionError>(GetWPCSession).Last();

			lastError.BGConsentId.ShouldBe(_failedConsent.Id);
			lastError.Code.ShouldBe(_failedConsent.InformationCode);
			lastError.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			lastError.HandledDate.ShouldBe(null);
			lastError.IsHandled.ShouldBe(false);
			lastError.Subscription.Id.ShouldBe(_subscription.Id);
			lastError.Type.ShouldBe(SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber);
		}

		[Test]
		public void Task_updates_recieved_consent_as_handled()
		{
			var consent = new BGReceivedConsentRepository(GetBGSession()).Get(_failedConsent.Id);
			consent.Handled.ShouldBe(true);
		}
	}
}