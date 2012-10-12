using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Test.Orders
{
	[TestFixture, Category("OrderTransactionTests")]
	public class When_fetching_an_old_transaction : GenericDataBase
	{
		private readonly SubscriptionTransaction _transaction;

		public When_fetching_an_old_transaction()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			var subscription = Persist(Factory.GetSubscription(shop, customer));
			_transaction = Persist(Factory.GetTransaction(subscription, 256.56M));
		}

		[Test]
		public void Amount_is_all_taxed()
		{
			var persistedTransaction = Fetch<SubscriptionTransaction>(_transaction.Id);
			persistedTransaction.GetAmount().Taxed.ShouldBe(256.56M);
			persistedTransaction.GetAmount().TaxFree.ShouldBe(0);
		}
	}

	[TestFixture, Category("OrderTransactionTests")]
	public class When_fetching_a_new_transaction : GenericDataBase
	{
		private readonly SubscriptionTransaction _transaction;

		public When_fetching_a_new_transaction()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			var subscription = Persist(Factory.GetSubscription(shop, customer));
			_transaction = Persist(Factory.GetTransaction(subscription, 256.56M, 350M));
		}

		[Test]
		public void Amount_has_taxed_and_tax_free_values()
		{
			var persistedTransaction = Fetch<SubscriptionTransaction>(_transaction.Id);
			persistedTransaction.GetAmount().Taxed.ShouldBe(256.56M);
			persistedTransaction.GetAmount().TaxFree.ShouldBe(350M);
		}

	}
}