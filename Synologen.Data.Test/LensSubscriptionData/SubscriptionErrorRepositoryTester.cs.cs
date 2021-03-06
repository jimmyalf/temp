using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData
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
				var shop = CreateShop(session);
					//new ShopRepository(session).Get(TestShopId);
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
				savedError.Code.ShouldBe(_errorToSave.Code);
				savedError.CreatedDate.ShouldBe(_errorToSave.CreatedDate);
				savedError.HandledDate.ShouldBe(_errorToSave.HandledDate);
				savedError.IsHandled.ShouldBe(_errorToSave.IsHandled);
				savedError.Subscription.ShouldBe(_errorToSave.Subscription);
				savedError.BGConsentId.ShouldBe(_errorToSave.BGConsentId);
				savedError.BGErrorId.ShouldBe(_errorToSave.BGErrorId);
				savedError.BGPaymentId.ShouldBe(_errorToSave.BGPaymentId);
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionErrorRepositoryTester")]
	public class When_editing_an_subscription_error : BaseRepositoryTester<SubscriptionErrorRepository>
	{

		private SubscriptionError _errorToEdit;

		public When_editing_an_subscription_error()
		{
			Context = session =>
			{
				var shop = CreateShop(session);
					// new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);

				var subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(subscription);

				var subscriptionErrorToSave = SubscriptionErrorFactory.Get(subscription);
				new SubscriptionErrorRepository(session).Save(subscriptionErrorToSave);
				_errorToEdit = SubscriptionErrorFactory.Edit(subscriptionErrorToSave);
			};

			Because = (SubscriptionErrorRepository repository) => repository.Save(_errorToEdit);
		}

		[Test]
		public void Should_edit_the_subscription_error()
		{
			AssertUsing(session =>
			{
				var savedError = new SubscriptionErrorRepository(session).Get(_errorToEdit.Id);

				savedError.ShouldBe(_errorToEdit);
				savedError.Type.ShouldBe(_errorToEdit.Type);
				savedError.Code.ShouldBe(_errorToEdit.Code);
				savedError.CreatedDate.ShouldBe(_errorToEdit.CreatedDate);
				savedError.HandledDate.ShouldBe(_errorToEdit.HandledDate);
				savedError.IsHandled.ShouldBe(_errorToEdit.IsHandled);
				savedError.Subscription.ShouldBe(_errorToEdit.Subscription);
				savedError.BGConsentId.ShouldBe(_errorToEdit.BGConsentId);
				savedError.BGErrorId.ShouldBe(_errorToEdit.BGErrorId);
				savedError.BGPaymentId.ShouldBe(_errorToEdit.BGPaymentId);
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionErrorRepositoryTester")]
	public class When_fetching_subscription_errors_by_AllUnhandledSubscriptionErrorsForShopCriteria : BaseRepositoryTester<SubscriptionErrorRepository>
	{
		private IList<SubscriptionError> _expectedErrorsForShop1;
		private IList<SubscriptionError> _expectedErrorsForShop2;
		private Shop _shop;

		public When_fetching_subscription_errors_by_AllUnhandledSubscriptionErrorsForShopCriteria()
		{

			Context = session =>
			{
				_shop = CreateShop(session);
					// new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, _shop);
				new CustomerRepository(session).Save(customer);
				var subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(subscription);
				_expectedErrorsForShop1 = SubscriptionErrorFactory.GetList(subscription);

				var shop2 = CreateShop(session);
					// new ShopRepository(session).Get(TestShop2Id);
				var customer2 = CustomerFactory.Get(country, shop2);
				new CustomerRepository(session).Save(customer2);
				var subscription2 = SubscriptionFactory.Get(customer2);
				new SubscriptionRepository(session).Save(subscription2);
				_expectedErrorsForShop2 = SubscriptionErrorFactory.GetList(subscription2);
			};
			Because = repository =>
			{
				_expectedErrorsForShop1.Each(repository.Save);
				_expectedErrorsForShop2.Each(repository.Save);
			}; 
		}

		[Test]
		public void Should_get_all_handled_subscription_errors_in_reversed_order()
		{
			var criteria = new AllUnhandledSubscriptionErrorsForShopCriteria(_shop.Id);
			var expectedErrors = _expectedErrorsForShop1.Where(x => Equals(x.HandledDate, null));
			AssertUsing( session =>
			{
				var fetchedErrors = new SubscriptionErrorRepository(session).FindBy(criteria);
				fetchedErrors.Count().ShouldBe(expectedErrors.Count());
				fetchedErrors.First().CreatedDate.ShouldBeGreaterThan(fetchedErrors.Last().CreatedDate);
			});
		}
	}
}