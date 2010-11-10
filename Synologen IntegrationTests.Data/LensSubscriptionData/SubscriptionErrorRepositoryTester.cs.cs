using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData
{

	[TestFixture]
	[Category("SubscriptionErrorRepositoryTester")]
	public class When_adding_an_subscription_error : BaseRepositoryTester<SubscriptionErrorRepository>
	{

		private SubscriptionError _errorToSave;

		public When_adding_an_subscription_error()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				var subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(subscription);
				_errorToSave = SubscriptionErrorFactory.Get(subscription);
			};

			Because = repository => repository.Save(_errorToSave);
		}

		[Test]
		public void Should_save_the_subscription_error()
		{
			AssertUsing(session =>
			{
				var savedError = new SubscriptionErrorRepository(session).Get(_errorToSave.Id);
				
				savedError.ShouldBe(_errorToSave);
				savedError.Type.ShouldBe(_errorToSave.Type);
				savedError.CreatedDate.ShouldBe(_errorToSave.CreatedDate);
				savedError.HandledDate.ShouldBe(_errorToSave.HandledDate);
				savedError.IsHandled.ShouldBe(_errorToSave.IsHandled);
				savedError.Subscription.ShouldBe(_errorToSave.Subscription);
			});
		}
	}
}
