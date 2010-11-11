using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	[TestFixture]
	[Category("ContractSalesRepositoryTester")]
	public class When_fetching_contract_sales_by_AllContractSalesMatchingCriteria : BaseRepositoryTester<ContractSaleRepository>
	{
		private IEnumerable<Order> _orders;
		private const int settlementableOrderStatus = 6;
		private const int nonSettlementableOrderStatus = 5;
		private const long testInvoiceNumber = 1865;

		public When_fetching_contract_sales_by_AllContractSalesMatchingCriteria()
		{
			Context = session =>
			{
				_orders = new[]
				{
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, null),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, null),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, testInvoiceNumber),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, null),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, null),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, testInvoiceNumber),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, testInvoiceNumber),
				};
			};

			Because = repository =>
			{
				_orders.Each(order =>
				{
					Provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
					Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
				});				
			};
		}

		[Test]
		public void Should_get_expected_items_matching_criteria_with_given_status_and_no_invoice_number()
		{
			var expectedContractSalesMatchingCriteria = _orders.Where(x => x.StatusId.Equals(settlementableOrderStatus) && Equals(x.InvoiceNumber, null));
		    var criteria = new AllContractSalesMatchingCriteria { ContractSaleStatus = settlementableOrderStatus, InvoiceNumber = null };
		    var matchingItems = GetResult(session => new ContractSaleRepository(session).FindBy(criteria));

			matchingItems.Count().ShouldBe(expectedContractSalesMatchingCriteria.Count());
			matchingItems.For((index,contractSale) =>
			{
				contractSale.Id.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).Id);
				contractSale.StatusId.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).StatusId);
				contractSale.InvoiceNumber.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).InvoiceNumber);
			});
		}

		[Test]
		public void Should_get_expected_items_matching_criteria_with_given_status_and_invoice_number()
		{
			var expectedContractSalesMatchingCriteria = _orders.Where(x => x.StatusId.Equals(settlementableOrderStatus) && Equals(x.InvoiceNumber, testInvoiceNumber));
		    var criteria = new AllContractSalesMatchingCriteria { ContractSaleStatus = settlementableOrderStatus, InvoiceNumber = testInvoiceNumber };
		    var matchingItems = GetResult(session => new ContractSaleRepository(session).FindBy(criteria));

			matchingItems.Count().ShouldBe(expectedContractSalesMatchingCriteria.Count());
			matchingItems.For((index,contractSale) =>
			{
				contractSale.Id.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).Id);
				contractSale.StatusId.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).StatusId);
				contractSale.InvoiceNumber.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).InvoiceNumber);
			});
		}
	}
}