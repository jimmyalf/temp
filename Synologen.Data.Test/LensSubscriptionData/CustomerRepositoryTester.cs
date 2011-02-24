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
	[Category("CustomerRepositoryTester")]
	public class When_adding_a_customer : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToSave;

		public When_adding_a_customer()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				_customerToSave = CustomerFactory.Get(country, shop);
			};

			Because = repository => repository.Save(_customerToSave);
		}

		[Test]
		public void Should_save_the_customer()
		{
			AssertUsing(session =>
			{
				var savedCustomer = new CustomerRepository(session).Get(_customerToSave.Id);
				savedCustomer.ShouldBe(_customerToSave);
				savedCustomer.Address.ShouldBe(_customerToSave.Address);
				savedCustomer.Contact.ShouldBe(_customerToSave.Contact);
				savedCustomer.FirstName.ShouldBe(_customerToSave.FirstName);
				savedCustomer.LastName.ShouldBe(_customerToSave.LastName);
				savedCustomer.PersonalIdNumber.ShouldBe(_customerToSave.PersonalIdNumber);
				savedCustomer.Shop.ShouldBe(_customerToSave.Shop);
				savedCustomer.Subscriptions.Count().ShouldBe(_customerToSave.Subscriptions.Count());
				savedCustomer.Notes.ShouldBe(_customerToSave.Notes);
			});
		}

	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_adding_a_customer_with_subscriptions : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToSave;
		private Subscription[] _subscriptionsToSave;

		public When_adding_a_customer_with_subscriptions()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				_customerToSave = CustomerFactory.Get(country, shop);
			};

			Because = repository =>
			{
				repository.Save(_customerToSave);
				_subscriptionsToSave = new[] { SubscriptionFactory.Get(_customerToSave),  SubscriptionFactory.Get(_customerToSave) };
				_subscriptionsToSave.Each( subscription => new SubscriptionRepository(GetSessionFactory().OpenSession()).Save(subscription));
			};
		}

		[Test]
		public void Should_save_the_customer_with_subscriptions()
		{
			AssertUsing(session =>
			{
				var savedCustomer = new CustomerRepository(session).Get(_customerToSave.Id);
				savedCustomer.Subscriptions.Count().ShouldBe(_subscriptionsToSave.Count());
			});
		}

	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_editing_a_customer : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToEdit;

		public When_editing_a_customer()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				_customerToEdit = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(_customerToEdit);
				_customerToEdit = CustomerFactory.Edit(_customerToEdit);

			};

			Because = repository => repository.Save(_customerToEdit);
		}

		[Test]
		public void Should_edit_the_customer()
		{
			AssertUsing(session =>
			{
				var savedCustomer = new CustomerRepository(session).Get(_customerToEdit.Id);
				savedCustomer.ShouldBe(_customerToEdit);
				savedCustomer.Address.ShouldBe(_customerToEdit.Address);
				savedCustomer.Contact.ShouldBe(_customerToEdit.Contact);
				savedCustomer.FirstName.ShouldBe(_customerToEdit.FirstName);
				savedCustomer.LastName.ShouldBe(_customerToEdit.LastName);
				savedCustomer.PersonalIdNumber.ShouldBe(_customerToEdit.PersonalIdNumber);
				savedCustomer.Shop.ShouldBe(_customerToEdit.Shop);
				savedCustomer.Subscriptions.Count().ShouldBe(_customerToEdit.Subscriptions.Count());
				savedCustomer.Notes.ShouldBe(_customerToEdit.Notes);
			});
		}

	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_deleting_a_customer : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToDelete;

		public When_deleting_a_customer()
		{
			Context = (ISession session) =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				_customerToDelete = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(_customerToDelete);
			};

			Because = repository => repository.Delete(_customerToDelete);
		}

		[Test]
		public void Should_delete_the_subscription()
		{
			AssertUsing(session =>
			{
				var fetchedCustomer = new CustomerRepository(session).Get(_customerToDelete.Id);
				fetchedCustomer.ShouldBe(null);
			});
		}
	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_fetching_all_customers : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToAdd1;
		private Customer _customerToAdd2;

		public When_fetching_all_customers()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				_customerToAdd1 = CustomerFactory.Get(country, shop);
				_customerToAdd2 = CustomerFactory.Get(country, shop);
			};

			Because = repository =>
			{
				repository.Save(_customerToAdd1);
				repository.Save(_customerToAdd2);
			};
		}

		[Test]
		public void Should_get_all_customers()
		{
			AssertUsing(session =>
			{
				var savedCustomers = new CustomerRepository(session).GetAll();
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd1.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd2.Id);
			});
		}

	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_fetching_all_customers_for_a_shop : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToAdd1;
		private Customer _customerToAdd2;
		private Customer _customerToAdd3;
		private Customer _customerToAdd4;

		public When_fetching_all_customers_for_a_shop()
		{
			Context = session =>
			{
				var shop1 = new ShopRepository(session).Get(TestShopId);
				var shop2 = new ShopRepository(session).Get(159);
				var country = new CountryRepository(session).Get(1);
				_customerToAdd1 = CustomerFactory.Get(country, shop1);
				_customerToAdd2 = CustomerFactory.Get(country, shop2);
				_customerToAdd3 = CustomerFactory.Get(country, shop1);
				_customerToAdd4 = CustomerFactory.Get(country, shop2);
			};

			Because = repository =>
			{
				repository.Save(_customerToAdd1);
				repository.Save(_customerToAdd2);
				repository.Save(_customerToAdd3);
				repository.Save(_customerToAdd4);
			};
		}

		[Test]
		public void Should_get_all_customers_for_a_shop()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria { ShopId = 159 };
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd2.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd4.Id);
				savedCustomers.Count().ShouldBe(2);
			});
		}
	}

	[TestFixture]
	[Category("CustomerRepositoryTester")]
	public class When_fetching_customers_for_a_shop_by_search : BaseRepositoryTester<CustomerRepository>
	{
		private Customer _customerToAdd1;
		private Customer _customerToAdd2;
		private Customer _customerToAdd3;
		private Customer _customerToAdd4;

		public When_fetching_customers_for_a_shop_by_search()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(159);
				var country = new CountryRepository(session).Get(TestCountryId);
				_customerToAdd1 = CustomerFactory.Get(country, shop, "Gunnar", "Gustafsson", "198206113411");
				_customerToAdd2 = CustomerFactory.Get(country, shop, "Katarina", "Malm", "198911063462");
				_customerToAdd3 = CustomerFactory.Get(country, shop, "Fredrik", "Holmberg", "197512235792");
				_customerToAdd4 = CustomerFactory.Get(country, shop, "Eva-Lisa", "Davidsson", "198007202826");
			};

			Because = repository =>
			{
				repository.Save(_customerToAdd1);
				repository.Save(_customerToAdd2);
				repository.Save(_customerToAdd3);
				repository.Save(_customerToAdd4);
			};
		}

		[Test]
		public void Should_get_customers_matching_firstname_search_for_a_shop()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "Gun"
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd1.Id);
				savedCustomers.Count().ShouldBe(1);
			});
		}

		[Test]
		public void Should_get_customers_matching_lastname_search_for_a_shop()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "sson"
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd1.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd4.Id);
				savedCustomers.Count().ShouldBe(2);
			});
		}

		[Test]
		public void Should_get_customers_matching_personalidnumber_search_for_a_shop()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "34"
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd1.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd2.Id);
				savedCustomers.Count().ShouldBe(2);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = ""
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd1.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd2.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd3.Id);
				savedCustomers.Select(x => x.Id).ShouldContain(_customerToAdd4.Id);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop_orderd_by_firstname_asc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "",
					OrderBy = "FirstName",
					SortAscending = true
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemName = savedCustomers.First().FirstName;
				var lastItemName = savedCustomers.Last().FirstName;
				firstItemName.ShouldBeLessThan(lastItemName);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop_orderd_by_firstname_desc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "",
					OrderBy = "FirstName",
					SortAscending = false
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemName = savedCustomers.First().FirstName;
				var lastItemName = savedCustomers.Last().FirstName;
				firstItemName.ShouldBeGreaterThan(lastItemName);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop_orderd_by_lastname_asc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "",
					OrderBy = "LastName",
					SortAscending = true
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemName = savedCustomers.First().FirstName;
				var lastItemName = savedCustomers.Last().FirstName;
				firstItemName.ShouldBeLessThan(lastItemName);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop_orderd_by_lastname_desc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "",
					OrderBy = "LastName",
					SortAscending = false
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemName = savedCustomers.First().LastName;
				var lastItemName = savedCustomers.Last().LastName;
				firstItemName.ShouldBeGreaterThan(lastItemName);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop_orderd_by_personalidnumber_asc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "",
					OrderBy = "PersonalIdNumber",
					SortAscending = true
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemNumber = savedCustomers.First().PersonalIdNumber;
				var lastItemNumber = savedCustomers.Last().PersonalIdNumber;
				firstItemNumber.ShouldBeLessThan(lastItemNumber);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_all_customers_search_for_a_shop_orderd_by_personalidnumber_desc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "",
					OrderBy = "PersonalIdNumber",
					SortAscending = false
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemNumber = savedCustomers.First().PersonalIdNumber;
				var lastItemNumber = savedCustomers.Last().PersonalIdNumber;
				firstItemNumber.ShouldBeGreaterThan(lastItemNumber);
				savedCustomers.Count().ShouldBe(4);
			});
		}

		[Test]
		public void Should_get_customers_matching_firstname_search_for_a_shop_orderd_by_lastname_desc()
		{
			AssertUsing(session =>
			{

				var criteria = new CustomersForShopMatchingCriteria
				{
					ShopId = 159,
					SearchTerm = "sson",
					OrderBy = "LastName",
					SortAscending = false
				};
				var savedCustomers = new CustomerRepository(session).FindBy(criteria);
				var firstItemName = savedCustomers.First().LastName;
				var lastItemName = savedCustomers.Last().LastName;
				firstItemName.ShouldBeGreaterThan(lastItemName);
				savedCustomers.Count().ShouldBe(2);
			});
		}
	}
}