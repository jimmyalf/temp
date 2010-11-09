using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.ShouldlyExtensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	[TestFixture]
	[Category("TestSettlement")]
	public class When_creating_a_settlement : TestBase<SettlementRepository>
	{
		private int _settlementId;
		private const int settlementableOrderStatus = 6;
		private const int nonSettlementableOrderStatus = 5;
		private const int orderStatusAfterSettlement = 8;
		private Order[] _ordersToSave;
		private double _expectedSumIncludingVAT;
		private double _expectedSumExcludingVAT;
		private int _expectedNumberOfOrdersInSettlement;

		public When_creating_a_settlement()
		{
			Context = session => 
			{
				SetupDefaultContext();
				_ordersToSave = new[]
				{
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, TestShop.ShopId, TestableShopMemberId),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId),
				};
				_ordersToSave.Each(order => {
					Provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
					Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
				});
				_expectedSumIncludingVAT = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Sum(x => x.InvoiceSumIncludingVAT);
				_expectedSumExcludingVAT = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Sum(x => x.InvoiceSumExcludingVAT);
				_expectedNumberOfOrdersInSettlement = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Count();
			};
			Because = repository =>
			{
				_settlementId = Provider.AddSettlement(settlementableOrderStatus, orderStatusAfterSettlement);
			};
		}

		[Test]
		public void Should_contain_expected_invoices_when_fetching_with_sql_data_provider()
		{
			float totalValueIncludingVAT;
			float totalValueExcludingVAT;
			var settlement = Provider.GetSettlement(_settlementId);
			settlement.NumberOfConnectedOrders.ShouldBe(_expectedNumberOfOrdersInSettlement);
			var settlementDetailsDataSet = Provider.GetSettlementDetailsDataSet(_settlementId, out totalValueIncludingVAT, out totalValueExcludingVAT, null);
			settlementDetailsDataSet.Tables[0].Rows.AsEnumerable().ForElementAtIndex(0, dataRow =>
			{
			    dataRow.Parse<int>("cId").ShouldBe(_settlementId);
			    dataRow.Parse<int>("cShopId").ShouldBe(TestShop.ShopId);
			    dataRow.Parse<string>("cShopNumber").ShouldBe(TestShop.Number);
			    dataRow.Parse<string>("cShopName").ShouldBe(TestShop.Name);
			    dataRow.Parse<string>("cGiroNumber").ShouldBe(TestShop.GiroNumber);
			    dataRow.Parse<double>("cPriceIncludingVAT").ShouldBe(_expectedSumIncludingVAT);
			    dataRow.Parse<double>("cPriceExcludingVAT").ShouldBe(_expectedSumExcludingVAT);
			    dataRow.Parse<int>("cNumberOfOrders").ShouldBe(_expectedNumberOfOrdersInSettlement);
			});
			totalValueIncludingVAT.ShouldBe((float) _expectedSumIncludingVAT);
			totalValueExcludingVAT.ShouldBe((float) _expectedSumExcludingVAT);
		}

		[Test]
		public void Should_contain_expected_invoices_when_fetching_with_nhibernate_repository()
		{
			 AssertUsing(session =>
			{
				var settlement = new SettlementRepository(session).Get(_settlementId);
				settlement.ContractSales.Count().ShouldBe(_expectedNumberOfOrdersInSettlement);
				settlement.ContractSales.Each(contractSale =>
				{
					contractSale.Shop.Id.ShouldBe(TestShop.ShopId);
					contractSale.Shop.BankGiroNumber.ShouldBe(TestShop.GiroNumber);
					contractSale.Shop.Name.ShouldBe(TestShop.Name);
					contractSale.Shop.Number.ShouldBe(TestShop.Number);
				});
				settlement.ContractSales.ForElementAtIndex(0, contractSale => 
				{
					contractSale.Id.ShouldBe(_ordersToSave.ElementAt(0).Id);
					contractSale.TotalAmountExcludingVAT.ShouldBe(_ordersToSave.ElementAt(0).InvoiceSumExcludingVAT.ToDecimal());
					contractSale.TotalAmountIncludingVAT.ShouldBe(_ordersToSave.ElementAt(0).InvoiceSumIncludingVAT.ToDecimal());

				});
				settlement.ContractSales.ForElementAtIndex(1, contractSale => 
				{
					contractSale.Id.ShouldBe(_ordersToSave.ElementAt(2).Id);
					contractSale.TotalAmountExcludingVAT.ShouldBe(_ordersToSave.ElementAt(2).InvoiceSumExcludingVAT.ToDecimal());
					contractSale.TotalAmountIncludingVAT.ShouldBe(_ordersToSave.ElementAt(2).InvoiceSumIncludingVAT.ToDecimal());
				});
				settlement.CreatedDate.ShouldBe(DateTime.Now, DateTimeTolerance.SameYearMonthDate);
				settlement.Id.ShouldBe(_settlementId);
				settlement.LensSubscriptionTransactions.ShouldBe(null);
			});
		}
	}
}