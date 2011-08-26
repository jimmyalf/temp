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
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories;
using Spinit.Wpc.Utility.Business;
using Shop=Spinit.Wpc.Synologen.Business.Domain.Entities.Shop;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales
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
		private double _expectedSumIncludingVATForShop1;
		private double _expectedSumExcludingVATForShop1;
		private double _expectedSumIncludingVAT;
		private double _expectedSumExcludingVAT;
		private int _expectedNumberOfOrdersInSettlement;
		private Subscription _subscription;
		private IList<SubscriptionTransaction> _transactions;
		private Article _article;
		private int _expectedNumberOfOrdersInSettlementForShop1;
		private IList<SubscriptionTransaction> _transactions2;
		private ContractArticleConnection _contractArticleConnection;
		private SqlProvider Provider;
		private Shop TestShop;

		protected override void SetUp()
		{
			Provider = new SqlProvider(DataHelper.ConnectionString);
			base.SetUp();
		}

		public When_creating_a_settlement_using_sqlprovider()
		{
			Context = session => 
			{
				_article = ArticleFactory.Get();
				Provider.AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
				_contractArticleConnection = ArticleFactory.GetContractArticleConnection(_article, TestableContractId, 999.23F, true);
				Provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref _contractArticleConnection);
				_ordersToSave = new List<Order>
				{
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId, _article.Id),
					OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId, _article.Id),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId, _article.Id),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId2, TestableShopMemberId, _article.Id),
					OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId2, TestableShop2MemberId, _article.Id),
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
				_expectedSumIncludingVATForShop1 = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus) && x.SalesPersonShopId.Equals(testableShopId)).Sum(x => x.InvoiceSumIncludingVAT);
				_expectedSumExcludingVATForShop1 = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus) && x.SalesPersonShopId.Equals(testableShopId)).Sum(x => x.InvoiceSumExcludingVAT);
				_expectedNumberOfOrdersInSettlementForShop1 = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus) && x.SalesPersonShopId.Equals(testableShopId)).Count();
				_expectedSumIncludingVAT = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Sum(x => x.InvoiceSumIncludingVAT);
				_expectedSumExcludingVAT = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Sum(x => x.InvoiceSumExcludingVAT);
				_expectedNumberOfOrdersInSettlement = _ordersToSave.Where(x => x.StatusId.Equals(settlementableOrderStatus)).Count();
				var shop = new ShopRepository(session).Get(testableShopId);
				var country = new CountryRepository(session).Get(TestCountryId);
				var customer = CustomerFactory.Get(country, shop);
				new CustomerRepository(session).Save(customer);
				_subscription = SubscriptionFactory.Get(customer);
				new SubscriptionRepository(session).Save(_subscription);
				_transactions = TransactionFactory.GetList(_subscription);

				var shop2 = new ShopRepository(session).Get(testableShopId2);
				var customer2 = CustomerFactory.Get(country, shop2);
				new CustomerRepository(session).Save(customer2);
				var subscription2 = SubscriptionFactory.Get(customer2);
				new SubscriptionRepository(session).Save(subscription2);
				_transactions2 = TransactionFactory.GetList(subscription2);
				
			};
			Because = repository =>
			{
				TestShop = Provider.GetShop(testableShopId);
				_settlementId = Provider.AddSettlement(settlementableOrderStatus, orderStatusAfterSettlement);
				var settlementMock = new Mock<Core.Domain.Model.LensSubscription.Settlement>();
				settlementMock.SetupGet(x => x.Id).Returns(_settlementId);
				var settlement = settlementMock.Object;
				var transactionRepository = new TransactionRepository(GetSessionFactory().OpenSession());
				_transactions.For((index, transaction) =>
				{
					transaction.Settlement = settlement;
					transactionRepository.Save(transaction);
				});

				_transactions2.For((index, transaction) =>
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
				dataRow.Parse<double>("cPriceIncludingVAT").ShouldBe(_expectedSumIncludingVATForShop1);
				dataRow.Parse<double>("cPriceExcludingVAT").ShouldBe(_expectedSumExcludingVATForShop1);
				dataRow.Parse<int>("cNumberOfOrders").ShouldBe(_expectedNumberOfOrdersInSettlementForShop1);
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
				settlement.ContractSales.ForElementAtIndex(0, contractSale => 
				{
					contractSale.Shop.Id.ShouldBe(testableShopId);
					contractSale.Shop.Name.ShouldBe(TestShop.Name);
					contractSale.Shop.Number.ShouldBe(TestShop.Number);
					contractSale.Shop.BankGiroNumber.ShouldBe(TestShop.GiroNumber);
					contractSale.Id.ShouldBe(_ordersToSave.ElementAt(0).Id);
					contractSale.TotalAmountIncludingVAT.ShouldBe(_ordersToSave.ElementAt(0).InvoiceSumIncludingVAT.ToDecimal());
				});
				settlement.ContractSales.ForElementAtIndex(1, contractSale => 
				{
					contractSale.Id.ShouldBe(_ordersToSave.ElementAt(2).Id);
					contractSale.TotalAmountIncludingVAT.ShouldBe(_ordersToSave.ElementAt(2).InvoiceSumIncludingVAT.ToDecimal());
				});
				settlement.LensSubscriptionTransactions.And(_transactions.Append(_transactions2).ToList()).Do((transaction, originalItem) => 
				{
					transaction.Amount.ShouldBe(originalItem.Amount);
					transaction.Id.ShouldBe(originalItem.Id);
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
			var expectedUniqueArticleQuanityForShop1 = _ordersToSave
				.Where(order => order.StatusId.Equals(settlementableOrderStatus) && order.SalesPersonShopId.Equals(testableShopId))
				.Sum(x => x.OrderItems.Sum(y => y.NumberOfItems));
			var expectedNumberOfRowsInDataSet = _ordersToSave.SelectMany(x => x.OrderItems).Select(x => x.ArticleId).Distinct().Count();
			var settlementDataSet = Provider.GetSettlementsOrderItemsDataSetSimple(testableShopId, _settlementId, null /*orderBy*/, out allOrdersMarkedAsPayed, out orderValueIncludingVAT, out orderValueExcludingVAT);

			settlementDataSet.Tables[0].Rows.Count.ShouldBe(expectedNumberOfRowsInDataSet);
			settlementDataSet.Tables[0].Rows.AsEnumerable().ForElementAtIndex(0, dataRow =>
			{
				dataRow.Parse<int>("cArticleId").ShouldBe(_article.Id);
				dataRow.Parse<string>("cArticleNumber").ShouldBe(_article.Number);
				dataRow.Parse<string>("cArticleName").ShouldBe(_article.Name);
				dataRow.Parse<int>("cNumberOfItems").ShouldBe(expectedUniqueArticleQuanityForShop1);
				dataRow.Parse<bool?>("cNoVAT").ShouldBe(_contractArticleConnection.NoVAT);
				dataRow.Parse<double>("cPriceSummary").ShouldBe(_expectedSumExcludingVATForShop1);
			});
			allOrdersMarkedAsPayed.ShouldBe(false);
			orderValueIncludingVAT.ShouldBe((float) _expectedSumIncludingVATForShop1);
			orderValueExcludingVAT.ShouldBe((float) _expectedSumExcludingVATForShop1);
		}

		[Test]
		public void Can_get_detailed_settlement_items_for_shop_using_sql_provider()
		{
			bool allOrdersMarkedAsPayed;
			float orderValueIncludingVAT;
			float orderValueExcludingVAT;
			var expectedNumberOfRowsInDataSet = _ordersToSave
				.Where(order => order.StatusId.Equals(settlementableOrderStatus)
				                && order.SalesPersonShopId.Equals(testableShopId))
				.SelectMany(x => x.OrderItems).Count();

			var settlementDataSet = Provider.GetSettlementsOrderItemsDataSetDetailed(
				testableShopId,
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
				dataRow.Parse<bool?>("cNoVAT").ShouldBe(_contractArticleConnection.NoVAT);
				dataRow.Parse<string>("cArticleNumber").ShouldBe(_article.Number);
				dataRow.Parse<string>("cArticleName").ShouldBe(_article.Name);
				dataRow.Parse<bool>("cOrderMarkedAsPayed").ShouldBe(false);
				dataRow.Parse<double>("cPriceSummary").ShouldBe(_ordersToSave.First().OrderItems.First().DisplayTotalPrice);
				dataRow.Parse<string>("cCompany").ShouldBe("Test Företag AB");
			});
			allOrdersMarkedAsPayed.ShouldBe(false);
			orderValueIncludingVAT.ShouldBe((float) _expectedSumIncludingVATForShop1);
			orderValueExcludingVAT.ShouldBe((float) _expectedSumExcludingVATForShop1);
		}

		[Test]
		public void Can_get_settlement_for_shop_using_nhibernate_contining_only_shop_specific_transactions_and_sale_items()
		{
			AssertUsing(session =>
			{
				var settlementForShop = new SettlementRepository(session).GetForShop(_settlementId, testableShopId);
				var expectedContractSales = _ordersToSave.Where(x => x.SalesPersonShopId.Equals(testableShopId) && x.StatusId.Equals(settlementableOrderStatus));
				var expectedSaleItems = expectedContractSales.SelectMany(x => x.OrderItems).ToList();
				settlementForShop.SaleItems.Count().ShouldBe(expectedSaleItems.Count());
				settlementForShop.LensSubscriptionTransactions.Count().ShouldBe(_transactions.Count());
				settlementForShop.ContractSalesValueIncludingVAT.ShouldBe((decimal)expectedContractSales.Sum(x => x.InvoiceSumIncludingVAT));
				settlementForShop.LensSubscriptionsValueIncludingVAT.ShouldBe(_transactions.Sum(x => x.Amount));
				settlementForShop.AllContractSalesHaveBeenMarkedAsPayed.ShouldBe(false);
				settlementForShop.SaleItems.And(expectedSaleItems).Do((saleItem, originalItem) =>
				{
					saleItem.Article.Name.ShouldBe(_article.Name);
					saleItem.Article.Number.ShouldBe(_article.Number);
					saleItem.Article.Id.ShouldBe(_article.Id);
					saleItem.Id.ShouldBe(originalItem.Id);
					saleItem.Quantity.ShouldBe(originalItem.NumberOfItems);
					saleItem.TotalPriceExcludingVAT().ShouldBe((decimal)originalItem.DisplayTotalPrice);
					saleItem.Sale.ContractCompany.Id.ShouldBe(TestableCompanyId);
					saleItem.Sale.ContractCompany.Name.ShouldBe("Test Företag AB");
					saleItem.Sale.ContractCompany.ContractId.ShouldBe(TestableContractId);
					saleItem.IsVATFree.ShouldBe(_contractArticleConnection.NoVAT);
				});
				settlementForShop.LensSubscriptionTransactions.And(_transactions).Do((transaction, originalItem) =>
				{
					transaction.CreatedDate.ShouldBe(originalItem.CreatedDate);
					transaction.Amount.ShouldBe(originalItem.Amount);
					transaction.Id.ShouldBe(originalItem.Id);
					transaction.Subscription.Id.ShouldBe(originalItem.Subscription.Id);
					transaction.Subscription.Customer.FirstName.ShouldBe(originalItem.Subscription.Customer.FirstName);
					transaction.Subscription.Customer.LastName.ShouldBe(originalItem.Subscription.Customer.LastName);
				});
			});
		}

		[Test]
		public void Can_get_settlement_for_shop_using_nhibernate_contining_only_shop_specific_transactions_and_sale_items_with_sale_items_marked_as_payed()
		{
			new SqlProvider(DataHelper.ConnectionString).MarkOrdersInSettlementAsPayedPerShop(_settlementId, testableShopId);
			AssertUsing(session =>
			{
				var settlementForShop = new SettlementRepository(session).GetForShop(_settlementId, testableShopId);
				settlementForShop.AllContractSalesHaveBeenMarkedAsPayed.ShouldBe(true);
			});
		}
	}
}