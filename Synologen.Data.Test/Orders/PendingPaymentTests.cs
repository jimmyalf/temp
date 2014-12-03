using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Test.Orders
{
	[TestFixture, Category("OrderPendingPaymentTests")]
	public class When_fetching_an_old_pending_payment : GenericDataBase
	{
		private readonly SubscriptionPendingPayment _pendingPayment;

		public When_fetching_an_old_pending_payment()
		{
			_pendingPayment = Persist(Factory.CreatePendingPayment(new SubscriptionAmount(255.45M, 87M)));
		}

		[Test]
		public void Amount_is_old_amount()
		{
			var pendingPayment = Fetch<SubscriptionPendingPayment>(_pendingPayment.Id);
			pendingPayment.GetValue().ShouldBe(new SubscriptionAmount(255.45M, 87M));
		}
	}

	[TestFixture, Category("OrderPendingPaymentTests")]
	public class When_fetching_a_new_pending_payment : GenericDataBase
	{
		private readonly SubscriptionPendingPayment _pendingPayment;
		private IEnumerable<SubscriptionItem> _subscriptions;

		public When_fetching_a_new_pending_payment()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			var subscription = Persist(Factory.GetSubscription(shop, customer));
			_subscriptions = PersistList(Factory.GetSubscriptionItems(subscription));
			_pendingPayment = Persist(Factory.CreatePendingPayment(_subscriptions));
		}

		[Test]
		public void Amount_is_a_sum_of_all_subscription_item_amounts()
		{
			var pendingPayment = Fetch<SubscriptionPendingPayment>(_pendingPayment.Id);
			pendingPayment.GetValue().ShouldBe(_subscriptions.Select(x => x.MonthlyWithdrawal).Sum());
		}
	}
}