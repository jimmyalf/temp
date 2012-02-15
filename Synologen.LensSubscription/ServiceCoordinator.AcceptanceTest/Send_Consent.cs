using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest.TestHelpers;
using SendConsentTask = Synologen.LensSubscription.ServiceCoordinator.Task.SendConsents.Task;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Sending Consent")]
	public class When_sending_a_consent : TaskBase
	{
		private SendConsentTask _task;
		private ITaskRunnerService _taskRunnerService;
		private IEnumerable<Subscription> _subscriptions;
		private OrderCustomer _customer;

		public When_sending_a_consent()
		{
			Context = () =>
			{
				var shopToUse = CreateShop<Shop>();
				_customer = StoreWithWpcSession(() => Factory.CreateCustomer(shopToUse));
				_subscriptions = StoreItemsWithWpcSession(() => Factory.CreateSubscriptions(_customer, shopToUse));
				StoreItemsWithWpcSession(() => Factory.CreateSubscriptions(_customer, shopToUse, consentStatus: SubscriptionConsentStatus.Accepted, sentDate: new DateTime(2011,01,01)));
				
				_task = ResolveTask<SendConsentTask>();
				_taskRunnerService = GetTaskRunnerService(_task);
			};

			Because = () => _taskRunnerService.Run();
		}

		[Test]
		public void Webservice_stores_consents()
		{
			var consents = GetAll<BGConsentToSend>(GetBGSession);
			consents.And(_subscriptions).Do((consent, subscription) =>
			{
				consent.Account.AccountNumber.ShouldBe(subscription.BankAccountNumber);
				consent.Account.ClearingNumber.ShouldBe(subscription.ClearingNumber);
				consent.OrgNumber.ShouldBe(null);
				consent.Payer.Id.ShouldBeGreaterThan(0);
				consent.PersonalIdNumber.ShouldBe(subscription.Customer.PersonalIdNumber);
				consent.SendDate.ShouldBe(null);
				consent.Type.ShouldBe(ConsentType.New);
			});
		}

		[Test]
		public void Task_updates_subscription_consent_status_as_sent_and_sets_payer_number()
		{
			var subscriptions = GetAll<Subscription>(GetWPCSession)
				.With(x => x.Id).In(_subscriptions, x => x.Id);
			foreach (var subscription in subscriptions)
			{
				subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Sent);
				subscription.AutogiroPayerId.ShouldBeGreaterThan(0);
			}
		}

		[Test]
		public void Autogiro_payers_were_created()
		{
			var agPayers = GetAll<AutogiroPayer>(GetBGSession);
			foreach (var autogiroPayer in agPayers)
			{
				autogiroPayer.Name.ShouldBe(_customer.FirstName + " " + _customer.LastName);	
				autogiroPayer.ServiceType.ShouldBe(AutogiroServiceType.SubscriptionVersion2);
			}
		}
	}
}