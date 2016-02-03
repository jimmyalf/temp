using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Test.Orders
{
	[TestFixture, Category("OrderTests")]
	public class When_fetching_an_old_order : GenericDataBase
	{
		private readonly Order _order;

		public When_fetching_an_old_order()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			_order = Persist(Factory.GetOrder(shop, customer, 275.23M));
		}

		[Test]
		public void Amount_is_all_taxed()
		{
			var persistedOrder = Fetch<Order>(_order.Id);
			persistedOrder.GetWithdrawalAmount().Taxed.ShouldBe(275.23M);
			persistedOrder.GetWithdrawalAmount().TaxFree.ShouldBe(0);
		}
	}

	[TestFixture, Category("OrderTests")]
	public class When_fetching_a_new_order : GenericDataBase
	{
		private readonly Order _order;

		public When_fetching_a_new_order()
		{
			var shop = CreateShop<Shop>();
			var customer = Persist(Factory.GetCustomer(shop));
			_order = Persist(Factory.GetOrder(shop, customer, 275.23M, 300.25M));
		}

		[Test]
		public void Amount_has_taxed_and_tax_free_values()
		{
			var persistedOrder = Fetch<Order>(_order.Id);
			persistedOrder.GetWithdrawalAmount().Taxed.ShouldBe(275.23M);
			persistedOrder.GetWithdrawalAmount().TaxFree.ShouldBe(300.25M);
		}
	}

}