using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.ShouldlyExtensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	[TestFixture]
	[Category("TestSettlement")]
	public class When_creating_a_settlement_using_sqlprovider : BaseRepositoryTester<SettlementRepository>
	{
		private int _settlementId;
		private const int settlementableOrderStatus = 6;
		private const int nonSettlementableOrderStatus = 5;
		private const int orderStatusAfterSettlement = 8;
		private Order[] _ordersToSave;
		private double _expectedSumIncludingVAT;
		private double _expectedSumExcludingVAT;
		private int _expectedNumberOfOrdersInSettlement;
		private Subscription _subscription;
		private SubscriptionTransaction[] _transactions;

		public When_creating_a_settlement_using_sqlprovider()
		{
			Context = session => 
			{
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
				var shop = new ShopRepository(session).Get(testableShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				_subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(_subscription);
				
			};
			Because = repository =>
			{
				_settlementId = Provider.AddSettlement(settlementableOrderStatus, orderStatusAfterSettlement);
				var settlementMock = new Mock<Core.Domain.Model.LensSubscription.Settlement>();
				settlementMock.SetupGet(x => x.Id).Returns(_settlementId);
				var settlement = settlementMock.Object;
				var transactionRepository = new TransactionRepository(base.GetSessionFactory().OpenSession());
				_transactions = TransactionFactory.GetList(_subscription);
				_transactions.For((index, transaction) =>
				{
					transaction.Settlement = settlement;
					transactionRepository.Save(transaction);
				});

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
				settlement.LensSubscriptionTransactions.For((index,transaction) => {
					transaction.Amount.ShouldBe(_transactions.ElementAt(index).Amount);
					transaction.Id.ShouldBe(_transactions.ElementAt(index).Id);
				});
				settlement.CreatedDate.ShouldBe(DateTime.Now, DateTimeTolerance.SameYearMonthDate);
				settlement.Id.ShouldBe(_settlementId);
			});
		}
	}
}