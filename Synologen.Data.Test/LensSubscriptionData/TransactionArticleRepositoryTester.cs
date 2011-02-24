using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.ShouldlyExtensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData
{
	[TestFixture]
	[Category("TransactionArticleRepositoryTester")]
	public class When_adding_a_transaction_article : BaseRepositoryTester<TransactionArticleRepository>
	{
		private TransactionArticle _articleToSave;

		public When_adding_a_transaction_article()
		{
			Context = session =>
			{
				_articleToSave = TransactionArticleFactory.Get();
			};
			Because = repository => repository.Save(_articleToSave);
		}

		[Test]
		public void Should_save_the_transaction_article()
		{
			AssertUsing(session =>
			{
				var savedArticle = new TransactionArticleRepository(session).Get(_articleToSave.Id);
				savedArticle.Id.ShouldBeGreaterThan(0);
				savedArticle.Name.ShouldBe(_articleToSave.Name);
				savedArticle.Active.ShouldBe(_articleToSave.Active);
				savedArticle.NumberOfConnectedTransactions.ShouldBe(0);
			});
		}
	}

	[TestFixture]
	[Category("TransactionArticleRepositoryTester")]
	public class When_connecting_an_article_to_a_transaction : BaseRepositoryTester<TransactionArticleRepository>
	{
		private TransactionArticle _articleToSave;
		private SubscriptionTransaction _transactionToSave;

		public When_connecting_an_article_to_a_transaction()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				var subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(subscription);
				_transactionToSave = TransactionFactory.Get(subscription);
				_articleToSave = TransactionArticleFactory.Get();
			};
			Because = repository =>
			{
				repository.Save(_articleToSave);
				_transactionToSave.Article = _articleToSave;
				new TransactionRepository(GetSessionFactory().OpenSession()).Save(_transactionToSave);
			};
		}

		[Test]
		public void Article_should_have_one_connected_transaction_()
		{
			AssertUsing(session=>
			{
				var savedArticle = new TransactionArticleRepository(session).Get(_articleToSave.Id);
				savedArticle.NumberOfConnectedTransactions.ShouldBe(1);
			});
		}
	}

	[TestFixture]
	[Category("TransactionArticleRepositoryTester")]
	public class When_fetching_transaction_articles_by_AllActiveTransactionArticlesCriteria : BaseRepositoryTester<TransactionArticleRepository>
	{
		private IList<TransactionArticle> _articlesToSave;
		private IList<TransactionArticle> _expectedArticlesMatchingCriteria;

		public When_fetching_transaction_articles_by_AllActiveTransactionArticlesCriteria()
		{
			Context = session =>
			{
				_articlesToSave = TransactionArticleFactory.GetList();
				_expectedArticlesMatchingCriteria = _articlesToSave.Where(x => x.Active).ToList();
			};

			Because = repository => _articlesToSave.Each(repository.Save);
		}

		[Test]
		public void Should_save_the_transaction_article()
		{
			var criteria = new AllActiveTransactionArticlesCriteria();
			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionArticleRepository(session).FindBy(criteria);
				itemsMatchingCriteria.ShouldBeSameLengthAs(_expectedArticlesMatchingCriteria);
				itemsMatchingCriteria.ForBoth(_expectedArticlesMatchingCriteria, (fetchedItem, expectedItem) =>
				{
					fetchedItem.Active.ShouldBe(expectedItem.Active);
					fetchedItem.Id.ShouldBe(expectedItem.Id);
					fetchedItem.Name.ShouldBe(expectedItem.Name);
					fetchedItem.NumberOfConnectedTransactions.ShouldBe(expectedItem.NumberOfConnectedTransactions);
				});
			});
		}
	}

	[TestFixture]
	[Category("TransactionArticleRepositoryTester")]
	public class When_fetching_transaction_articles_by_PageOfTransactionArticlesMatchingCriteria : BaseRepositoryTester<TransactionArticleRepository>
	{
		private IList<TransactionArticle> _articlesToSave;
		private IList<SubscriptionTransaction> _transactionsToSave;

		public When_fetching_transaction_articles_by_PageOfTransactionArticlesMatchingCriteria()
		{
			Context = session =>
			{
				var shop = new ShopRepository(session).Get(TestShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				var subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(subscription);
				_transactionsToSave = TransactionFactory.GetList(subscription);
				_articlesToSave = TransactionArticleFactory.GetList(50);
			};

			Because = repository =>
			{
				_articlesToSave.Each(repository.Save);
				var transactionRepository = new TransactionRepository(GetSessionFactory().OpenSession());
				_transactionsToSave.Each(transaction =>
				{
					transaction.Article = _articlesToSave[25];
					transactionRepository.Save(transaction);
				});
				
			};
		}

		[Test]
		public void Can_get_expected_number_of_items_matching_page_size()
		{
			var criteria = new PageOfTransactionArticlesMatchingCriteria{ PageSize = 2 };
			var matchingItems = GetResult(session => new TransactionArticleRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBe(criteria.PageSize);
		}

		[Test]
		public void Can_get_expected_items_ordered_by_id()
		{
			var criteria = new PageOfTransactionArticlesMatchingCriteria { OrderBy = "Id", PageSize = 100, SortAscending = false};
			var matchingItems = GetResult(session => new TransactionArticleRepository(session).FindBy(criteria));
			matchingItems.First().Id.ShouldBe(_articlesToSave.Count());
			matchingItems.Last().Id.ShouldBe(1);
		}

		[Test]
		public void Can_get_expected_items_ordered_by_name()
		{
			var criteria = new PageOfTransactionArticlesMatchingCriteria { OrderBy = "Name", PageSize = 100, SortAscending = false};
			var matchingItems = GetResult(session => new TransactionArticleRepository(session).FindBy(criteria));
			matchingItems.First().Name.ShouldBe("Artikel Z");
			matchingItems.Last().Name.ShouldBe("Artikel A");
		}

		[Test]
		public void Can_get_expected_items_ordered_by_active()
		{
			var criteria = new PageOfTransactionArticlesMatchingCriteria { OrderBy = "Active", PageSize = 100, SortAscending = false};
			var matchingItems = GetResult(session => new TransactionArticleRepository(session).FindBy(criteria));
			matchingItems.First().Active.ShouldBe(true);
			matchingItems.Last().Active.ShouldBe(false);
		}

		[Test]
		public void Can_get_expected_items_ordered_by_number_of_connected_transactions()
		{
			var criteria = new PageOfTransactionArticlesMatchingCriteria { OrderBy = "NumberOfConnectedTransactions", PageSize = 100, SortAscending = false};
			var matchingItems = GetResult(session => new TransactionArticleRepository(session).FindBy(criteria));
			matchingItems.First().NumberOfConnectedTransactions.ShouldBe(_transactionsToSave.Count());
			matchingItems.Last().NumberOfConnectedTransactions.ShouldBe(0);
		}

		[Test]
		public void Can_get_expected_items_when_searching_for_name()
		{
			var expectedNumberOfResults = _articlesToSave.Where(x => x.Name.Equals("Artikel A")).Count();
			var criteria = new PageOfTransactionArticlesMatchingCriteria { SearchTerm = "Artikel A",  PageSize = 100 };
			var matchingItems = GetResult(session => new TransactionArticleRepository(session).FindBy(criteria));
			matchingItems.Count().ShouldBe(expectedNumberOfResults);
			foreach (var item in matchingItems)
			{
				item.Name.ShouldContain(criteria.SearchTerm);
			}
		}

	}
}