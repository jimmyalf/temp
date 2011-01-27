using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData
{
	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_adding_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription _subscriptionToSave;

		public When_adding_a_subscription()
		{
			Context = (ISession session) =>
          	{
          		var shop = new ShopRepository(session).Get(TestShopId);
          		var country = new CountryRepository(session).Get(TestCountryId);
          		var customer = CustomerFactory.Get(country, shop);
          		new CustomerRepository(session).Save(customer);
          		_subscriptionToSave = SubscriptionFactory.Get(customer);
          	};

			Because = (SubscriptionRepository repository) => repository.Save(_subscriptionToSave);
		}

		[Test]
		public void Should_save_the_subscription()
		{
			AssertUsing(session => 
        	{
        		var savedSubscription = new SubscriptionRepository(session).Get(_subscriptionToSave.Id);
        		savedSubscription.ShouldBe(_subscriptionToSave);
        		savedSubscription.ActivatedDate.ShouldBe(_subscriptionToSave.ActivatedDate);
        		savedSubscription.CreatedDate.ShouldBe(_subscriptionToSave.CreatedDate);
        		savedSubscription.Customer.ShouldBe(_subscriptionToSave.Customer);
        		savedSubscription.PaymentInfo.ShouldBe(_subscriptionToSave.PaymentInfo);
        		savedSubscription.Status.ShouldBe(_subscriptionToSave.Status);
        		savedSubscription.Transactions.Count().ShouldBe(_subscriptionToSave.Transactions.Count());
				savedSubscription.Errors.Count().ShouldBe(_subscriptionToSave.Errors.Count());
				savedSubscription.Notes.ShouldBe(_subscriptionToSave.Notes);
				savedSubscription.ConsentStatus.ShouldBe(_subscriptionToSave.ConsentStatus);
        	});
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_editing_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription _subscriptionToEdit;

		public When_editing_a_subscription()
		{
			Context = (ISession session) =>
          	{
          		var shop = new ShopRepository(session).Get(TestShopId);
          		var country = new CountryRepository(session).Get(TestCountryId);
          		var customer = CustomerFactory.Get(country, shop);
          		new CustomerRepository(session).Save(customer);
          		var subscriptionToSave = SubscriptionFactory.Get(customer);
          		new SubscriptionRepository(session).Save(subscriptionToSave);
          		_subscriptionToEdit = SubscriptionFactory.Edit(subscriptionToSave);
          	};

			Because = (SubscriptionRepository repository) => repository.Save(_subscriptionToEdit);
		}

		[Test]
		public void Should_edit_the_subscription()
		{
			AssertUsing(session => 
        	{
        		var fetchedSubscription = new SubscriptionRepository(session).Get(_subscriptionToEdit.Id);
        		fetchedSubscription.ShouldBe(_subscriptionToEdit);
        		fetchedSubscription.ActivatedDate.ShouldBe(_subscriptionToEdit.ActivatedDate);
        		fetchedSubscription.CreatedDate.ShouldBe(_subscriptionToEdit.CreatedDate);
        		fetchedSubscription.Customer.ShouldBe(_subscriptionToEdit.Customer);
        		fetchedSubscription.PaymentInfo.ShouldBe(_subscriptionToEdit.PaymentInfo);
        		fetchedSubscription.Status.ShouldBe(_subscriptionToEdit.Status);
        		fetchedSubscription.Transactions.Count().ShouldBe(_subscriptionToEdit.Transactions.Count());
				fetchedSubscription.Errors.Count().ShouldBe(_subscriptionToEdit.Errors.Count());
				fetchedSubscription.Notes.ShouldBe(_subscriptionToEdit.Notes);
				fetchedSubscription.ConsentStatus.ShouldBe(_subscriptionToEdit.ConsentStatus);
        	});
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_deleting_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription _subscriptionToDelete;

		public When_deleting_a_subscription()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				_subscriptionToDelete = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(_subscriptionToDelete);
			};

			Because = repository => repository.Delete(_subscriptionToDelete);
		}

		[Test]
		public void Should_delete_the_subscription()
		{
			AssertUsing(session =>
			{
				var fetchedSubscription = new SubscriptionRepository(session).Get(_subscriptionToDelete.Id);
				fetchedSubscription.ShouldBe(null);
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_fetching_subscriptions_by_PageOfSubscriptionsMatchingCriteria : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription[] _savedSubscriptions;

		public When_fetching_subscriptions_by_PageOfSubscriptionsMatchingCriteria()
		{
			Context = session =>
			{
				var shop1 = new ShopRepository(session).Get(TestShopId);
				var shop2 = new ShopRepository(session).Get(TestShop2Id);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customers = new[]
				{
					CustomerFactory.Get(country, shop1, "Gunnar", "Gustafsson", "198206113411"),
					CustomerFactory.Get(country, shop2, "Katarina", "Malm", "198911063462"),
					CustomerFactory.Get(country, shop1, "Fredrik", "Holmberg", "197512235792"),
					CustomerFactory.Get(country, shop2, "Eva-Lisa", "Davidsson", "198007202826"),
				};
				customers.Each(new CustomerRepository(session).Save);
				_savedSubscriptions = customers.Select(customer => SubscriptionFactory.Get(customer, SubscriptionStatus.Active))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, SubscriptionStatus.Stopped))).ToArray();
			
			};

			Because = repository => _savedSubscriptions.Each(repository.Save);
		}

		[Test]
		public void Should_get_expected_number_of_items_matching_page_size()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { PageSize = 2 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBe(criteria.PageSize);
		}

		[Test]
		public void Should_get_expected_items_ordered_by_customer_first_name()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { OrderBy = "Customer.FirstName", PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			var firstItemName = matchingItems.First().Customer.FirstName;
			var lastItemName = matchingItems.Last().Customer.FirstName;
			firstItemName.ShouldBeLessThan(lastItemName);
		}

		[Test]
		public void Should_get_expected_items_ordered_by_customer_last_name()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { OrderBy = "Customer.LastName", PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			var firstItemName = matchingItems.First().Customer.LastName;
			var lastItemName = matchingItems.Last().Customer.LastName;
			firstItemName.ShouldBeLessThan(lastItemName);
		}

		[Test]
		public void Should_get_expected_items_ordered_by_customer_shop_name()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { OrderBy = "Customer.Shop.Name", PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			var firstItemName = matchingItems.First().Customer.Shop.Name;
			var lastItemName = matchingItems.Last().Customer.Shop.Name;
			firstItemName.ShouldBeLessThan(lastItemName);
		}

		[Test]
		public void Should_get_expected_items_ordered_by_status()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { OrderBy = "Status", PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			var firstItemStatus = matchingItems.First().Status;
			var lastItemStatus = matchingItems.Last().Status;
			firstItemStatus.ShouldBeLessThan(lastItemStatus);
		}

		[Test]
		public void Should_get_expected_items_when_searching_for_last_names()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { SearchTerm = "sson",  PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBeGreaterThan(0);
			foreach (var item in matchingItems)
			{
				item.Customer.LastName.ShouldContain(criteria.SearchTerm);
			}
		}

		[Test]
		public void Should_get_expected_items_when_searching_for_first_names()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { SearchTerm = "Eva-Lisa",  PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBeGreaterThan(0);
			foreach (var item in matchingItems)
			{
				item.Customer.FirstName.ShouldContain(criteria.SearchTerm);
			}
		}

		[Test]
		public void Should_get_expected_items_when_searching_for_personal_id_number()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { SearchTerm = "11",  PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBeGreaterThan(0);
			foreach (var item in matchingItems)
			{
				item.Customer.PersonalIdNumber.ShouldContain(criteria.SearchTerm);
			}
		}

		[Test]
		public void Should_get_expected_items_when_searching_for_shop_name()
		{
			var criteria = new PageOfSubscriptionsMatchingCriteria { SearchTerm = "bågbeställning",  PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBeGreaterThan(0);
			foreach (var item in matchingItems)
			{
				item.Customer.Shop.Name.ShouldContain(criteria.SearchTerm);
			}
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_fetching_all_transactions_for_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private IList<SubscriptionTransaction> _transactions;
		private Subscription _subscription;

		public When_fetching_all_transactions_for_a_subscription()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);

				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				_subscription = SubscriptionFactory.Get(customer);

				new SubscriptionRepository(session).Save(_subscription);

				_transactions = TransactionFactory.GetList(_subscription);
			};

			Because = repository => _transactions.Each(x => new TransactionRepository(GetSessionFactory().OpenSession()).Save(x));
			
		}

		[Test]
		public void Should_get_all_transactions_for_a_subscription()
		{
			AssertUsing(session =>
			{
				var savedSubscription = new SubscriptionRepository(session).Get(_subscription.Id);
				savedSubscription.Transactions.ShouldContain(_transactions[0]);
				savedSubscription.Transactions.ShouldContain(_transactions[1]);
				savedSubscription.Transactions.ShouldContain(_transactions[2]);
				savedSubscription.Transactions.ShouldContain(_transactions[3]);
				savedSubscription.Transactions.Count().ShouldBe(4);
			});
		}
	}

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_fetching_all_errors_for_a_subscription : BaseRepositoryTester<SubscriptionErrorRepository>
	{

		private SubscriptionError[] _errors;
		private Subscription _subscription;

		public When_fetching_all_errors_for_a_subscription()
		{
			Context = session =>
          	{
          		var shop = new ShopRepository(session).Get(TestShopId);
          		var country = new CountryRepository(session).Get(TestCountryId);

          		var customer = CustomerFactory.Get(country, shop);
          		new CustomerRepository(session).Save(customer);
          		_subscription = SubscriptionFactory.Get(customer);

				new SubscriptionRepository(session).Save(_subscription);

          		_errors = SubscriptionErrorFactory.GetList(_subscription);
          	};

			Because = repository => _errors.Each(x => new SubscriptionErrorRepository(GetSessionFactory().OpenSession()).Save(x));
		}

		[Test]
		public void Should_get_all_errors_for_a_subscription()
		{
			AssertUsing(session =>
			{
				var savedSubscription = new SubscriptionRepository(session).Get(_subscription.Id);

				for (int i = 0; i < 6; i++)
				{
					savedSubscription.Errors.ShouldContain(_errors[i]);
				}
				savedSubscription.Errors.Count().ShouldBe(6);
			});
		}
	}
}