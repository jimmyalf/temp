using System;
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
using Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData
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
				savedSubscription.Active.ShouldBe(_subscriptionToSave.Active);
				savedSubscription.Transactions.Count().ShouldBe(_subscriptionToSave.Transactions.Count());
				savedSubscription.Errors.Count().ShouldBe(_subscriptionToSave.Errors.Count());
				savedSubscription.Notes.ShouldBe(_subscriptionToSave.Notes);
				savedSubscription.ConsentStatus.ShouldBe(_subscriptionToSave.ConsentStatus);
				savedSubscription.PaymentInfo.PaymentSentDate.ShouldBe(_subscriptionToSave.PaymentInfo.PaymentSentDate);
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
				fetchedSubscription.Active.ShouldBe(_subscriptionToEdit.Active);
				fetchedSubscription.Transactions.Count().ShouldBe(_subscriptionToEdit.Transactions.Count());
				fetchedSubscription.Errors.Count().ShouldBe(_subscriptionToEdit.Errors.Count());
				fetchedSubscription.Notes.ShouldBe(_subscriptionToEdit.Notes);
				fetchedSubscription.ConsentStatus.ShouldBe(_subscriptionToEdit.ConsentStatus);
				fetchedSubscription.PaymentInfo.PaymentSentDate.ShouldBe(_subscriptionToEdit.PaymentInfo.PaymentSentDate);
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
				bool isActive = true;
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

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_fetching_subscriptions_by_AllSubscriptionsToSendConsentsForCriteria : BaseRepositoryTester<SubscriptionRepository>
	{
		private IList<Subscription> _savedSubscriptions;
		private const bool Subscription_Is_Active = true;
		private const bool Subscription_Not_Active = false;

		public When_fetching_subscriptions_by_AllSubscriptionsToSendConsentsForCriteria()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
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

	[TestFixture]
	[Category("SubscriptionRepositoryTester")]
	public class When_fetching_subscriptions_by_AllSubscriptionsToSendPaymentsForCriteria : BaseRepositoryTester<SubscriptionRepository>
	{
		private IList<Subscription> _savedSubscriptions;
		private const bool Subscription_Is_Active = true;
		private const bool Subscription_Not_Active = false;
		private DateTime dateWithOtherMonth;

		public When_fetching_subscriptions_by_AllSubscriptionsToSendPaymentsForCriteria()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop, "Gunnar", "Gustafsson", "198206113411");
				new CustomerRepository(session).Save(customer);
				dateWithOtherMonth = GetDateWithOtherMonth();

				_savedSubscriptions = new List<Subscription>
				{
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Accepted, null),
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Accepted, dateWithOtherMonth),
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Accepted, DateTime.Now.AddYears(-1)),
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Accepted, DateTime.Now),
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Denied, dateWithOtherMonth),
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.NotSent, null),
					SubscriptionFactory.Get(customer, Subscription_Is_Active, SubscriptionConsentStatus.Sent, dateWithOtherMonth),
					SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.Accepted, dateWithOtherMonth),
					SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.Denied, null),
					SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.NotSent, null),
					SubscriptionFactory.Get(customer, Subscription_Not_Active, SubscriptionConsentStatus.Sent, null)
				};
			};

			Because = repository => _savedSubscriptions.Each(repository.Save);
		}

		private static DateTime GetDateWithOtherMonth()
		{
			DateTime present = DateTime.Now;
			return (present.Month == 1) ? new DateTime(present.Year, present.Month + 1, present.Day) : present.AddMonths(-1); 
		}


		[Test]
		public void Criteria_only_fetches_active_subscriptions_with_consent_and_paymentsentdate_empty_or_other_month()
		{
			AssertUsing(session =>
			{
				var subscriptions = new SubscriptionRepository(session).FindBy(new AllSubscriptionsToSendPaymentsForCriteria()).ToArray();
				subscriptions.Count().ShouldBe(3);

				Subscription subscription = subscriptions[0];
				subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Accepted);
				subscription.Active.ShouldBe(Subscription_Is_Active);
				subscription.PaymentInfo.PaymentSentDate.ShouldBe(null);

				subscription = subscriptions[1];
				subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Accepted);
				subscription.Active.ShouldBe(Subscription_Is_Active);
				subscription.PaymentInfo.PaymentSentDate.Value.Year.ShouldBe(DateTime.Now.Year);
				subscription.PaymentInfo.PaymentSentDate.Value.Month.ShouldBe(dateWithOtherMonth.Month);

				subscription = subscriptions[2];
				subscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.Accepted);
				subscription.Active.ShouldBe(Subscription_Is_Active);
				subscription.PaymentInfo.PaymentSentDate.Value.Year.ShouldBe(DateTime.Now.Year - 1);
				subscription.PaymentInfo.PaymentSentDate.Value.Month.ShouldBe(DateTime.Now.Month);
			});
		}
	}
}