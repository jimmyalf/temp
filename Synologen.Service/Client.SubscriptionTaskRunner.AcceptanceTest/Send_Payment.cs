using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.ServiceCoordinator.Task.SendPayments;
using Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest.TestHelpers;

namespace Synologen.Service.Client.SubscriptionTaskRunner.AcceptanceTest
{
	[TestFixture, Category("Feature: Sending Payment")]
	public class When_sending_a_payment : TaskBase
	{
		private ITaskRunnerService _taskRunnerService;
		private Task _task;
		private OrderCustomer _customer;
		private Subscription _subscription;
		private int _bankGiroPayerNumber;
		private DateTime _expectedPaymentDate;
		private IEnumerable<SubscriptionItem> _subscriptionItems;
		private decimal _expectedPaymentAmount;

		public When_sending_a_payment()
		{
			Context = () =>
			{
				InvokeWebService(service =>
				{
					_bankGiroPayerNumber = service.RegisterPayer("Test payer", AutogiroServiceType.SubscriptionVersion2);
				});

				_expectedPaymentDate = CalculatePaymentDate();
				var shopToUse = CreateShop<Shop>();
				_customer = StoreWithWpcSession(() =>Factory.CreateCustomer(shopToUse));
				_subscription = StoreWithWpcSession(() => Factory.CreateSubscription(_customer, shopToUse, _bankGiroPayerNumber, Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Accepted, new DateTime(2011, 01, 01)));
				_subscriptionItems = StoreItemsWithWpcSession(() => Factory.CreateSubscriptionItems(_subscription));
				_task = ResolveTask<Task>();
				_taskRunnerService = GetTaskRunnerService(_task);
				_expectedPaymentAmount = _subscriptionItems.Where(x => x.IsActive).Sum(x => x.AmountForAutogiroWithdrawal);
			};

			Because = () => _taskRunnerService.Run();
		}

		[Test]
		public void Webservice_stores_payment()
		{
			var pendingPayment = GetAll<SubscriptionPendingPayment>(GetWPCSession).Single();
			var payment = GetAll<BGPaymentToSend>(GetBGSession).Single();
			payment.Amount.ShouldBe(_expectedPaymentAmount);
			payment.HasBeenSent.ShouldBe(false);
			payment.Payer.Id.ShouldBe(_bankGiroPayerNumber);
			payment.PaymentDate.Date.ShouldBe(_expectedPaymentDate);
			payment.PaymentPeriodCode.ShouldBe(PaymentPeriodCode.PaymentOnceOnSelectedDate);
			payment.Reference.ShouldBe(pendingPayment.Id.ToString());
			payment.SendDate.ShouldBe(null);
			payment.Type.ShouldBe(PaymentType.Debit);
		}

		[Test]
		public void Task_creates_a_pending_payment()
		{
			var pendingPayment = GetAll<SubscriptionPendingPayment>(GetWPCSession).Single();
			pendingPayment.Amount.ShouldBe(_expectedPaymentAmount);
			pendingPayment.Created.Date.ShouldBe(SystemTime.Now.Date);
			pendingPayment.HasBeenPayed.ShouldBe(false);
			_subscriptionItems.Where(x => x.IsActive).Each(subscriptionItem => 
				pendingPayment.SubscriptionItems.ShouldContain(x => x.Id == subscriptionItem.Id)
			);

		}

		[Test]
		public void Task_updates_payment_date()
		{
			var fetchedSubscription = Get<Subscription>(GetWPCSession, _subscription.Id);
			fetchedSubscription.LastPaymentSent.Value.Date.ShouldBe(_expectedPaymentDate);
		}

		private DateTime CalculatePaymentDate()
		{
			var expectedPaymentDay = ResolveEntity<IServiceCoordinatorSettingsService>().GetPaymentDayInMonth();
			var cutOffDayInMonth = ResolveEntity<IServiceCoordinatorSettingsService>().GetPaymentCutOffDayInMonth();
			var date = new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, expectedPaymentDay);
			if(SystemTime.Now.Day >= cutOffDayInMonth)
			{
				date = date.AddMonths(1);
			}
			return date;
		}
	}
}