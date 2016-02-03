using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Data.Test.Orders
{
	[TestFixture, Category("SubscriptionItemTests")]
	public class When_persisting_a_new_subscription_item : GenericDataBase
	{
		private readonly SubscriptionItem _subscriptionItem;

		public When_persisting_a_new_subscription_item()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			var subscription = Persist(Factory.GetSubscription(shop, customer));
			_subscriptionItem = Persist(Factory.GetSubscriptionItem(subscription));
		}

		[Test]
		public void Subscription_item_has_expected_version_tag()
		{
			var subscription = Fetch<SubscriptionItem>(_subscriptionItem.Id);
			subscription.Version.ShouldBe(_subscriptionItem.Version);
		}		
	}

	[TestFixture, Category("SubscriptionItemTests")]
	public class When_persisting_an_old_subscription_item : GenericDataBase
	{
		private readonly SubscriptionItem _subscriptionItem;

		public When_persisting_an_old_subscription_item()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			var subscription = Persist(Factory.GetSubscription(shop, customer));
			_subscriptionItem = Persist(Factory.GetSubscriptionItem(subscription, version: SubscriptionVersion.VersionOne));
		}

		[Test]
		public void Subscription_item_has_expected_version_tag()
		{
			var subscriptionItem = Fetch<SubscriptionItem>(_subscriptionItem.Id);
			subscriptionItem.Version.ShouldBe(_subscriptionItem.Version);
		}		
	}
}