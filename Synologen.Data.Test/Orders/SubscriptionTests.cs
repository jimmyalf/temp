using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Data.Test.Orders
{
	[TestFixture, Category("SubscriptionTests")]
	public class When_persisting_a_new_subscription : GenericDataBase
	{
		private readonly Subscription _subscription;

		public When_persisting_a_new_subscription()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			_subscription = Persist(Factory.GetSubscription(shop, customer));
		}

		[Test]
		public void Subscription_has_expected_version_tag()
		{
			var subscription = Fetch<Subscription>(_subscription.Id);
			subscription.Version.ShouldBe(SubscriptionVersion.VersionTwo);
		}
	}

	[TestFixture, Category("SubscriptionTests")]
	public class When_persisting_an_old_subscription : GenericDataBase
	{
		private readonly Subscription _subscription;

		public When_persisting_an_old_subscription()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			_subscription = Persist(Factory.GetSubscription(shop, customer, version: SubscriptionVersion.VersionOne));
		}

		[Test]
		public void Subscription_has_expected_version_tag()
		{
			var subscription = Fetch<Subscription>(_subscription.Id);
			subscription.Version.ShouldBe(SubscriptionVersion.VersionOne);
		}
	}
}