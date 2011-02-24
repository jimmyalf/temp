using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales
{
	[TestFixture]
	[Category("ContractSalesRepositoryTester")]
	public class When_fetching_contract_sales_by_AllContractSalesMatchingCriteria : BaseRepositoryTester<ContractSaleRepository>
	{
		private IEnumerable<Order> _orders;
		private const int settlementableOrderStatus = 6;
		private const int nonSettlementableOrderStatus = 5;
		private ISqlProvider Provider;

		protected override void SetUp()
		{
			Provider = new SqlProvider(DataHelper.ConnectionString);
			base.SetUp();
		}

		public When_fetching_contract_sales_by_AllContractSalesMatchingCriteria()
		{
			Context = session =>
			{
				_orders = new[]
				{
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
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
		public void Should_get_expected_items_matching_criteria_with_given_status()
		{
			var expectedContractSalesMatchingCriteria = _orders.Where(x => x.StatusId.Equals(settlementableOrderStatus));
			var criteria = new AllContractSalesMatchingCriteria { ContractSaleStatus = settlementableOrderStatus };//, InvoiceNumber = null };
			var matchingItems = GetResult(session => new ContractSaleRepository(session).FindBy(criteria));

			matchingItems.Count().ShouldBe(expectedContractSalesMatchingCriteria.Count());
			matchingItems.For((index,contractSale) =>
			{
				contractSale.Id.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).Id);
				contractSale.StatusId.ShouldBe(criteria.ContractSaleStatus);
			});
		}
	}
}