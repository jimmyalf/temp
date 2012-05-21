using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories;
using AllSubscriptionsToSendConsentsForCriteria = Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription.AllSubscriptionsToSendConsentsForCriteria;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData
{
	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_adding_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription _subscriptionToSave;

		public When_adding_a_subscription()
		{
			Context = (ISession session) =>
			{
				var shop = CreateShop(session);
					// new ShopRepository(session).Get(TestShopId);
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
				savedSubscription.Active.ShouldBe(_subscriptionToSave.Active);
				savedSubscription.Transactions.Count().ShouldBe(_subscriptionToSave.Transactions.Count());
				savedSubscription.Errors.Count().ShouldBe(_subscriptionToSave.Errors.Count());
				savedSubscription.Notes.ShouldBe(_subscriptionToSave.Notes);
				savedSubscription.ConsentStatus.ShouldBe(_subscriptionToSave.ConsentStatus);
				savedSubscription.PaymentInfo.PaymentSentDate.ShouldBe(_subscriptionToSave.PaymentInfo.PaymentSentDate);
				savedSubscription.BankgiroPayerNumber.ShouldBe(_subscriptionToSave.BankgiroPayerNumber);
			});
		}

		[Test]
		public void Can_fetch_subscription_by_bankgiro_number()
		{
			AssertUsing(session => 
			{
				var subscriptionById = new SubscriptionRepository(session).Get(_subscriptionToSave.Id);
				var subscriptionByBankgiroNumber = new SubscriptionRepository(session).GetByBankgiroPayerId(_subscriptionToSave.BankgiroPayerNumber.Value);
				subscriptionByBankgiroNumber.ShouldBe(subscriptionById);
			});
		}
	}

	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_editing_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription _subscriptionToEdit;

		public When_editing_a_subscription()
		{
			Context = (ISession session) =>
			{
				var shop = CreateShop(session);
					// new ShopRepository(session).Get(TestShopId);
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
				fetchedSubscription.Active.ShouldBe(_subscriptionToEdit.Active);
				fetchedSubscription.Transactions.Count().ShouldBe(_subscriptionToEdit.Transactions.Count());
				fetchedSubscription.Errors.Count().ShouldBe(_subscriptionToEdit.Errors.Count());
				fetchedSubscription.Notes.ShouldBe(_subscriptionToEdit.Notes);
				fetchedSubscription.ConsentStatus.ShouldBe(_subscriptionToEdit.ConsentStatus);
				fetchedSubscription.PaymentInfo.PaymentSentDate.ShouldBe(_subscriptionToEdit.PaymentInfo.PaymentSentDate);
				fetchedSubscription.BankgiroPayerNumber.ShouldBe(_subscriptionToEdit.BankgiroPayerNumber);
			});
		}
	}

	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_deleting_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription _subscriptionToDelete;

		public When_deleting_a_subscription()
		{
			Context = session =>
			{
				var shop = CreateShop(session);
					// new ShopRepository(session).Get(TestShopId);
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

	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_fetching_subscriptions_by_PageOfSubscriptionsMatchingCriteria : BaseRepositoryTester<SubscriptionRepository>
	{
		private Subscription[] _savedSubscriptions;

		public When_fetching_subscriptions_by_PageOfSubscriptionsMatchingCriteria()
		{
			Context = session =>
			{
				const bool isActive = true;
				var shop1 = CreateShop(session);
					//= new ShopRepository(session).Get(TestShopId);
				var shop2 = CreateShop(session, shopName: "Testbutik 2");
					//= new ShopRepository(session).Get(TestShop2Id);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customers = new[]
				{
					CustomerFactory.Get(country, shop1, "Gunnar", "Gustafsson", "198206113411"),
					CustomerFactory.Get(country, shop2, "Katarina", "Malm", "198911063462"),
					CustomerFactory.Get(country, shop1, "Fredrik", "Holmberg", "197512235792"),
					CustomerFactory.Get(country, shop2, "Eva-Lisa", "Davidsson", "198007202826"),
				};
				customers.Each(new CustomerRepository(session).Save);
				_savedSubscriptions = customers.Select(customer => SubscriptionFactory.Get(customer, isActive))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, !isActive))).ToArray();
			
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
			var criteria = new PageOfSubscriptionsMatchingCriteria { OrderBy = "Active", PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			var firstItemActiveStatus = matchingItems.First().Active;
			var lastItemActiveStatus = matchingItems.Last().Active;
			firstItemActiveStatus.ShouldBeLessThan(lastItemActiveStatus);
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
			var criteria = new PageOfSubscriptionsMatchingCriteria { SearchTerm = "Testbutik 2",  PageSize = 100 };
			var matchingItems = GetResult(session => new SubscriptionRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBeGreaterThan(0);
			foreach (var item in matchingItems)
			{
				item.Customer.Shop.Name.ShouldContain(criteria.SearchTerm);
			}
		}
	}

	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_fetching_all_transactions_for_a_subscription : BaseRepositoryTester<SubscriptionRepository>
	{
		private IList<SubscriptionTransaction> _transactions;
		private Subscription _subscription;

		public When_fetching_all_transactions_for_a_subscription()
		{
			Context = session =>
			{
				var shop = CreateShop(session); 
					//new ShopRepository(session).Get(TestShopId);
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

	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_fetching_all_errors_for_a_subscription : BaseRepositoryTester<SubscriptionErrorRepository>
	{

		private SubscriptionError[] _errors;
		private Subscription _subscription;

		public When_fetching_all_errors_for_a_subscription()
		{
			Context = session =>
			{
				var shop = CreateShop(session); 
					// new ShopRepository(session).Get(TestShopId);
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

	[TestFixture, Category("SubscriptionRepositoryTester"), Ignore("")]
	public class When_fetching_subscriptions_by_AllSubscriptionsToSendConsentsForCriteria : BaseRepositoryTester<SubscriptionRepository>
	{
		private IList<Subscription> _savedSubscriptions;
		private const bool Subscription_Is_Active = true;
		private const bool Subscription_Not_Active = false;

		public When_fetching_subscriptions_by_AllSubscriptionsToSendConsentsForCriteria()
		{
			Context = session =>
			{
				var shop = CreateShop(session); 
					// new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customers = new[]
				{
					CustomerFactory.Get(country, shop, "Gunnar", "Gustafsson", "198206113411"),
					CustomerFactory.Get(country, shop, "Katarina", "Malm", "198911063462"),
					CustomerFactory.Get(country, shop, "Fredrik", "Holmberg", "197512235792"),
					CustomerFactory.Get(country, shop, "Eva-Lisa", "Davidsson", "198007202826"),
				};
				customers.Each(new CustomerRepository(session).Save);
				_savedSubscriptions = customers
					.Select(customer => SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Accepted))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Denied)))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.NotSent)))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Sent)))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.Accepted)))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.Denied)))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.NotSent)))
					.Append(customers.Select(customer => SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.Sent)))
					.ToArray();
			
			};

			Because = repository => _savedSubscriptions.Each(repository.Save);
		}
		
		[Test]
		public void Criteria_only_fetches_active_subscriptions_with_consent_not_sent()
		{
			AssertUsing(session =>
			{
				var subscriptions = new SubscriptionRepository(session).FindBy(new AllSubscriptionsToSendConsentsForCriteria());
				subscriptions.Count().ShouldBe(4);
				subscriptions.Each(subscription =>
				{
					subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.NotSent);
					subscription.Active.ShouldBe(Subscription_Is_Active);
				});
			});
		}
	}

	[TestFixture, Category("SubscriptionRepositoryTester")]
	public class When_fetching_subscriptions_by_AllSubscriptionsToSendPaymentsForCriteria : BaseRepositoryTester<SubscriptionRepository>
	{
		private IList<Subscription> _savedSubscriptions;
		private IEnumerable<Subscription> fetchedSubscriptions;
		private DateTime currentDate;
		private DateTime firstDayInCurrentMonth;
		private const bool Active = true;
		private const bool InActive = false;

		public When_fetching_subscriptions_by_AllSubscriptionsToSendPaymentsForCriteria()
		{
			Context = session =>
			{
				var shop = CreateShop(session); 
					// new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop, "Gunnar", "Gustafsson", "198206113411");
				new CustomerRepository(session).Save(customer);
				currentDate = new DateTime(2011, 03, 24);
				firstDayInCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

				_savedSubscriptions = new List<Subscription>
				{
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, null), // --> Should be included
					SubscriptionFactory.Get(customer, InActive, SubscriptionConsentStatus.Accepted, null),
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, new DateTime(2010,05,25)), // --> Should be included
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, new DateTime(2011,01,25)), // --> Should be included
					SubscriptionFactory.Get(customer, InActive, SubscriptionConsentStatus.Accepted, new DateTime(2011,01,25)),
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, new DateTime(2011,02,25)), // --> Should be included
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, new DateTime(2011,03,20)),
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, new DateTime(2011,03,25)),
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Accepted, new DateTime(2011,04,25)),
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.NotSent, new DateTime(2011,02,25)),
					SubscriptionFactory.Get(customer, Active, SubscriptionConsentStatus.Denied, new DateTime(2011,02,25)),
				};
			};

			Because = repository =>
			{
				_savedSubscriptions.Each(repository.Save);
				fetchedSubscriptions = SystemTime.ReturnWhileTimeIs(currentDate, () => repository.FindBy(new AllSubscriptionsToSendPaymentsForCriteria()));
			};
		}

		[Test]
		public void Criteria_fetches_active_subscriptions()
		{
			fetchedSubscriptions.Each(subscription => subscription.Active.ShouldBe(true));
		}

		[Test]
		public void Criteria_fetches_subscriptions_with_consent()
		{
			fetchedSubscriptions.Each(subscription => subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Accepted));
		}

		[Test]
		public void Criteria_fetches_subscriptions_with_paymentsentdate_empty_or_from_any_previous_months()
		{
			fetchedSubscriptions.Each(subscription =>
			{
				if(subscription.PaymentInfo.PaymentSentDate.HasValue)
				{
					subscription.PaymentInfo.PaymentSentDate.Value.ShouldBeLessThan(firstDayInCurrentMonth);
				}
			});
		}
	}
}