using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.ShouldlyExtensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
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
		private IList<Order> _ordersToSave;
		private double _expectedSumIncludingVAT;
		private double _expectedSumExcludingVAT;
		private int _expectedNumberOfOrdersInSettlement;
		private Subscription _subscription;
		private SubscriptionTransaction[] _transactions;
		private Article _article;

		public When_creating_a_settlement_using_sqlprovider()
		{
			Context = session => 
			{
				_article = ArticleFactory.Get();
				Provider.AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
				var contractArticleConnection = ArticleFactory.GetContractArticleConnection(_article, TestableContractId, 999.23F);
				Provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref contractArticleConnection);
				_ordersToSave = new List<Order>
				{
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, _article.Id),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, _article.Id),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, TestShop.ShopId, TestableShopMemberId, _article.Id),
				};
				_ordersToSave.Each(order => 
				{
					Provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
					Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
					order.OrderItems.Each(orderItem =>
					{
						IOrderItem tempOrder = orderItem;
						tempOrder.OrderId = order.Id;
						Provider.AddUpdateDeleteOrderItem(Enumerations.Action.Create, ref tempOrder);
					});
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
			var settlement = Provider.GetSettlement(_settlementId);
			settlement.NumberOfConnectedOrders.ShouldBe(_expectedNumberOfOrdersInSettlement);
			settlement.CreatedDate.ShouldBe(DateTime.Now, DateTimeTolerance.SameYearMonthDate);
		}

		[Test]
		public void Can_get_detailed_settlement_items_using_sql_data_provider()
		{
			float totalValueIncludingVAT;
			float totalValueExcludingVAT;
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
					contractSale.TotalAmountIncludingVAT.ShouldBe(_ordersToSave.ElementAt(0).InvoiceSumIncludingVAT.ToDecimal());

				});
				settlement.ContractSales.ForElementAtIndex(1, contractSale => 
				{
					contractSale.Id.ShouldBe(_ordersToSave.ElementAt(2).Id);
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

		[Test]
		public void Can_get_simple_settlement_items_for_shop_using_sql_provider()
		{
			bool allOrdersMarkedAsPayed;
			float orderValueIncludingVAT;
			float orderValueExcludingVAT;
			var expectedUniqueArticleQuanity = _ordersToSave
				.Where(order => order.StatusId.Equals(settlementableOrderStatus))
				.Sum(x => x.OrderItems.Sum(y => y.NumberOfItems));
			var expectedNumberOfRowsInDataSet = _ordersToSave.SelectMany(x => x.OrderItems).Select(x => x.ArticleId).Distinct().Count();
			var settlementDataSet = Provider.GetSettlementsOrderItemsDataSetSimple(TestShop.ShopId, _settlementId, null /*orderBy*/, out allOrdersMarkedAsPayed, out orderValueIncludingVAT, out orderValueExcludingVAT);

			settlementDataSet.Tables[0].Rows.Count.ShouldBe(expectedNumberOfRowsInDataSet);
			settlementDataSet.Tables[0].Rows.AsEnumerable().ForElementAtIndex(0, dataRow =>
			{
			    dataRow.Parse<int>("cArticleId").ShouldBe(_article.Id);
				dataRow.Parse<string>("cArticleNumber").ShouldBe(_article.Number);
				dataRow.Parse<string>("cArticleName").ShouldBe(_article.Name);
				dataRow.Parse<int>("cNumberOfItems").ShouldBe(expectedUniqueArticleQuanity);
				dataRow.Parse<bool?>("cNoVAT").ShouldBe(false);
				dataRow.Parse<double>("cPriceSummary").ShouldBe(_expectedSumExcludingVAT);
			});
			allOrdersMarkedAsPayed.ShouldBe(false);
			orderValueIncludingVAT.ShouldBe((float) _expectedSumIncludingVAT);
			orderValueExcludingVAT.ShouldBe((float) _expectedSumExcludingVAT);
		}

		[Test]
		public void Can_get_detailed_settlement_items_for_shop_using_sql_provider()
		{
			bool allOrdersMarkedAsPayed;
			float orderValueIncludingVAT;
			float orderValueExcludingVAT;
			var expectedNumberOfRowsInDataSet = _ordersToSave.Where(order => order.StatusId.Equals(settlementableOrderStatus))
				.SelectMany(x => x.OrderItems).Count();

			var settlementDataSet = Provider.GetSettlementsOrderItemsDataSetDetailed(
				TestShop.ShopId,
				_settlementId,
				null /*orderBy*/,
				out allOrdersMarkedAsPayed,
				out orderValueIncludingVAT,
				out orderValueExcludingVAT);
			settlementDataSet.Tables[0].Rows.Count.ShouldBe(expectedNumberOfRowsInDataSet);
			settlementDataSet.Tables[0].Rows.AsEnumerable().ForElementAtIndex(0, dataRow =>
			{
				dataRow.Parse<int>("cOrderId").ShouldBe(_ordersToSave.First().Id);
				dataRow.Parse<int>("cArticleId").ShouldBe(_article.Id);
				dataRow.Parse<int>("cNumberOfItems").ShouldBe(_ordersToSave.First().OrderItems.First().NumberOfItems);
				dataRow.Parse<bool?>("cNoVAT").ShouldBe(false);
				dataRow.Parse<string>("cArticleNumber").ShouldBe(_article.Number);
				dataRow.Parse<string>("cArticleName").ShouldBe(_article.Name);
				dataRow.Parse<bool>("cOrderMarkedAsPayed").ShouldBe(false);
				dataRow.Parse<double>("cPriceSummary").ShouldBe(_ordersToSave.First().OrderItems.First().DisplayTotalPrice);
				dataRow.Parse<string>("cCompany").ShouldBe("Test Företag AB");
			});
			allOrdersMarkedAsPayed.ShouldBe(false);
			orderValueIncludingVAT.ShouldBe((float) _expectedSumIncludingVAT);
			orderValueExcludingVAT.ShouldBe((float) _expectedSumExcludingVAT);
		}
	}
}