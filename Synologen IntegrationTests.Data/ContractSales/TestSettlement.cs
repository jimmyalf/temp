using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	[TestFixture]
	[Category("TestSettlement")]
	public class When_creating_a_settlement : TestBase
	{
		private readonly int _settlementId;
		private const int settlementableOrderStatus = 6;
		private const int nonSettlementableOrderStatus = 5;
		private const int orderStatusAfterSettlement = 8;
		private readonly Order[] _ordersToSave;

		public When_creating_a_settlement()
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
			_settlementId = Provider.AddSettlement(settlementableOrderStatus, orderStatusAfterSettlement);
		}

		[Test]
		public void Should_contain_expected_invoices()
		{
			var expectedSumIncludingVAT = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Sum(x => x.InvoiceSumIncludingVAT);
			var expectedSumExcludingVAT = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Sum(x => x.InvoiceSumExcludingVAT);
			var expectedNumberOfOrdersInSettlement = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Count();

			float totalValueIncludingVAT;
			float totalValueExcludingVAT;
			var settlement = Provider.GetSettlement(_settlementId);
			settlement.NumberOfConnectedOrders.ShouldBe(expectedNumberOfOrdersInSettlement);
			var settlementDetailsDataSet = Provider.GetSettlementDetailsDataSet(_settlementId, out totalValueIncludingVAT, out totalValueExcludingVAT, null);
			var dataRow = settlementDetailsDataSet.Tables[0].Rows[0];
			dataRow.Parse<int>("cId").ShouldBe(_settlementId);
			dataRow.Parse<int>("cShopId").ShouldBe(TestShop.ShopId);
			dataRow.Parse<string>("cShopNumber").ShouldBe(TestShop.Number);
			dataRow.Parse<string>("cShopName").ShouldBe(TestShop.Name);
			dataRow.Parse<string>("cGiroNumber").ShouldBe(TestShop.GiroNumber);
			dataRow.Parse<double>("cPriceIncludingVAT").ShouldBe(expectedSumIncludingVAT);
			dataRow.Parse<double>("cPriceExcludingVAT").ShouldBe(expectedSumExcludingVAT);
			dataRow.Parse<int>("cNumberOfOrders").ShouldBe(expectedNumberOfOrdersInSettlement);
			totalValueIncludingVAT.ShouldBe((float) expectedSumIncludingVAT);
			totalValueExcludingVAT.ShouldBe((float) expectedSumExcludingVAT);
		}
	}
}