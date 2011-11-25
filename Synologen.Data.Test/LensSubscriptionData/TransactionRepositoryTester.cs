using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData
{
	[TestFixture]
	[Category("TransactionRepositoryTester")]
	public class When_adding_a_transaction : BaseRepositoryTester<TransactionRepository>
	{

		private SubscriptionTransaction _transactionToSave;

		public When_adding_a_transaction()
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
				_transactionToSave = TransactionFactory.Get(subscription);
			};

			Because = repository => repository.Save(_transactionToSave);
		}

		[Test]
		public void Should_save_the_transaction()
		{
			AssertUsing(session =>
			{
				var savedTransaction = new TransactionRepository(session).Get(_transactionToSave.Id);
				savedTransaction.ShouldBe(_transactionToSave);
				savedTransaction.Amount.ShouldBe(_transactionToSave.Amount);
				savedTransaction.CreatedDate.ShouldBe(_transactionToSave.CreatedDate);
				savedTransaction.Subscription.ShouldBe(_transactionToSave.Subscription);
				savedTransaction.Reason.ShouldBe(_transactionToSave.Reason);
				savedTransaction.Type.ShouldBe(_transactionToSave.Type);
			});
		}
	}

	[TestFixture]
	[Category("TransactionRepositoryTester")]
	public class When_fetching_transactions_by_TransactionsForSubscriptionsMatchingCriteria : BaseRepositoryTester<TransactionRepository>
	{
		private IList<SubscriptionTransaction> _transactions;
		private IList<SubscriptionTransaction> _otherTransactions;
		private Subscription _subscription;
		private Subscription _otherSubscription;
		private Settlement _settlement;
		private const int settlementableOrderStatus = 6;
		private const int afterSettlementOrderStatus = 8;

		public When_fetching_transactions_by_TransactionsForSubscriptionsMatchingCriteria()
		{
			Context = session =>
			{
				var shop = CreateShop(session);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
     
				_subscription = SubscriptionFactory.Get(customer);
				_otherSubscription = SubscriptionFactory.Get(customer);
				var subscriptionRepository = new SubscriptionRepository(session);
				subscriptionRepository.Save(_subscription);
				subscriptionRepository.Save(_otherSubscription);
				var settlementId = Provider.AddSettlement(settlementableOrderStatus, afterSettlementOrderStatus);
				var settlement = new Mock<Settlement>();
				settlement.SetupGet(x => x.Id).Returns(settlementId);
				_settlement = settlement.Object;

				_transactions = TransactionFactory.GetList(_subscription);
				_otherTransactions = TransactionFactory.GetList(_otherSubscription);

				Because = repository =>
				{
					_transactions.For((index, transaction) =>
					{
						transaction.Settlement = (index % 2 == 0) ? _settlement : null;
						repository.Save(transaction);
					});

					_otherTransactions.For((index, transaction) =>
					{
						transaction.Settlement = (index % 2 == 0) ? _settlement : null;
						repository.Save(transaction);
					});

				};

			};
		}

		[Test]
		public void Should_get_expected_items_filtered_by_subscription_and_sorted_by_createdate()
		{
			var criteria = new TransactionsForSubscriptionMatchingCriteria { SubscriptionId = _subscription.Id };
			var expectedTransactions = _transactions.OrderByDescending(x => x.CreatedDate).ToArray();

			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionRepository(session).FindBy(criteria);
				itemsMatchingCriteria.Count().ShouldBe(expectedTransactions.Count());
				itemsMatchingCriteria.Each(transaction => transaction.Subscription.Id.ShouldBe(_subscription.Id));
				itemsMatchingCriteria.For((index, transaction) => transaction.Amount.ShouldBe(expectedTransactions.ElementAt(index).Amount));
				itemsMatchingCriteria.For((index, transaction) => transaction.CreatedDate.ShouldBe(expectedTransactions.ElementAt(index).CreatedDate));
				itemsMatchingCriteria.For((index, transaction) => transaction.Reason.ShouldBe(expectedTransactions.ElementAt(index).Reason));
				itemsMatchingCriteria.For((index, transaction) => transaction.Type.ShouldBe(expectedTransactions.ElementAt(index).Type));
				itemsMatchingCriteria.For((index, transaction) => transaction.With(x => x.Settlement).Return(x => x.Id, Int32.MinValue)
				                                                  	.ShouldBe(expectedTransactions.ElementAt(index).With(x => x.Settlement).Return(x => x.Id, Int32.MinValue)));
				
			});
		}
	}

	[TestFixture]
	[Category("ContractSalesRepositoryTester")]
	public class When_fetching_transactions_by_AllTransactionsMatchingCriteria : BaseRepositoryTester<TransactionRepository>
	{
		private IList<SubscriptionTransaction> _transactions;
		private Settlement _settlement;
		private const int settlementableOrderStatus = 6;
		private const int afterSettlementOrderStatus = 8;

		public When_fetching_transactions_by_AllTransactionsMatchingCriteria()
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
				var settlementId = Provider.AddSettlement(settlementableOrderStatus, afterSettlementOrderStatus);			
				_transactions = TransactionFactory.GetList(subscription);
				var settlement = new Mock<Settlement>();
				settlement.SetupGet(x => x.Id).Returns(settlementId);
				_settlement = settlement.Object;
			};

			Because = repository =>
			{
				_transactions.For((index, transaction) =>
				{
					transaction.Settlement = (index % 2 == 0) ? _settlement : null;
					repository.Save(transaction);
				});
			};
		}

		[Test]
		public void Should_get_expected_items_filtered_by_having_settlement()
		{
			var criteria = new AllTransactionsMatchingCriteria { SettlementStatus = SettlementStatus.HasSettlement };
			var expectedMatchingTransactions = _transactions.Where(x => Equals(x.Settlement, null).Equals(false));
			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionRepository(session).FindBy(criteria);
				itemsMatchingCriteria.For((index,transaction) => transaction.Settlement.Id.ShouldBe(expectedMatchingTransactions.ElementAt(index).Settlement.Id));
			});
		}

		[Test]
		public void Should_get_expected_items_filtered_by_not_having_settlement()
		{
			var criteria = new AllTransactionsMatchingCriteria { SettlementStatus = SettlementStatus.DoesNotHaveSettlement };
			var expectedMatchingTransactions = _transactions.Where(x => Equals(x.Settlement, null));
			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionRepository(session).FindBy(criteria);
				itemsMatchingCriteria.Count().ShouldBe(expectedMatchingTransactions.Count());
				itemsMatchingCriteria.For((index,transaction) => transaction.Settlement.ShouldBe(null));
			});
		}

		[Test]
		public void Should_get_expected_items_filtered_by_any_settlement()
		{
			var criteria = new AllTransactionsMatchingCriteria { SettlementStatus = SettlementStatus.Any };
			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionRepository(session).FindBy(criteria);
				itemsMatchingCriteria.Count().ShouldBe(_transactions.Count());
				itemsMatchingCriteria.For((index,transaction) => 
				                          transaction.With(x => x.Settlement).Return(x => x.Id, Int32.MinValue)
				                          	.ShouldBe(_transactions.ElementAt(index).With(x => x.Settlement).Return(x => x.Id, Int32.MinValue)));
			});
		}

		[Test]
		public void Should_get_expected_items_filtered_by_reason()
		{
			var criteria = new AllTransactionsMatchingCriteria
			{
				SettlementStatus = SettlementStatus.Any,
				Reason = TransactionReason.Payment
			};
			var expectedMatchingItems = _transactions.Where(x => x.Reason.Equals(criteria.Reason));
			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionRepository(session).FindBy(criteria);
				itemsMatchingCriteria.Count().ShouldBe(expectedMatchingItems.Count());
				itemsMatchingCriteria.Each(transaction => transaction.Reason.ShouldBe(criteria.Reason.Value));
			});
		}

		[Test]
		public void Should_get_expected_items_filtered_by_type()
		{
			var criteria = new AllTransactionsMatchingCriteria
			{
				SettlementStatus = SettlementStatus.Any,
				Type = TransactionType.Withdrawal
			};
			var expectedMatchingItems = _transactions.Where(x => x.Type.Equals(criteria.Type));
			AssertUsing(session =>
			{
				var itemsMatchingCriteria = new TransactionRepository(session).FindBy(criteria);
				itemsMatchingCriteria.Count().ShouldBe(expectedMatchingItems.Count());
				itemsMatchingCriteria.Each(transaction => transaction.Type.ShouldBe(criteria.Type.Value));
			});
		}
	}
}