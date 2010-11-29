using System;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test
{
	[SetUpFixture]
	public class GlobalTestBase
	{
		protected int TestCountryId = 1;
		protected int TestShopId = 158;
		protected int TestShop2Id = 159;

		[SetUp]
		public void RunBeforeAnyTests()
		{
			if (NHibernateFactory.MappingAssemblies.Any()) return;
			var assembly = typeof(Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions.SubscriptionMap).Assembly;
			NHibernateFactory.MappingAssemblies.Add(assembly);
		}

		[TearDown]
		public void RunAfterAnyTests()
		{
			SetupLensSubscriptionData();
			SetupContractSaleSettlementData();
		}

		private void SetupContractSaleData() 
		{ 
			var provider = new SqlProvider(DataHelper.ConnectionString);
			if(String.IsNullOrEmpty(DataHelper.ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!IsDevelopmentServer(DataHelper.ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			const int settlementableOrderStatus = 6;
			const int nonSettlementableOrderStatus = 5;
			const int testableShopId = 158;
			const int TestableShopMemberId = 485;
			const int TestableCompanyId = 57;
			const int TestableContractId = 14;

			var article = ArticleFactory.Get();
			provider.AddUpdateDeleteArticle(Enumerations.Action.Create, ref article);
			var contractArticleConnection = ArticleFactory.GetContractArticleConnection(article, TestableContractId, 999.23F);
			provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref contractArticleConnection);

			var orders = new[]
			{
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
				OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
				OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
				OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId, article.Id),
			};
			orders.Each(order =>
			{
				provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
				provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
				order.OrderItems.Each(orderItem =>
				{
					IOrderItem tempOrder = orderItem;
					tempOrder.OrderId = order.Id;
					provider.AddUpdateDeleteOrderItem(Enumerations.Action.Create, ref tempOrder);
				});
			});				
		}

		private void SetupContractSaleSettlementData() 
		{ 
			var provider = new SqlProvider(DataHelper.ConnectionString);
			
			const int settlementableOrderStatus = 6;
			const int orderStatusAfterSettlement = 8;
			Action action  = () => 
			{
				SetupContractSaleData();
				provider.AddSettlement(settlementableOrderStatus, orderStatusAfterSettlement);	
			};
			action.Times(5);
			SetupContractSaleData();
		}


		private void SetupLensSubscriptionData() {
			var session = GetSessionFactory().OpenSession();
			var shop1 = new ShopRepository(session).Get(TestShopId);
			var shop2 = new ShopRepository(session).Get(TestShop2Id);
			var country = new CountryRepository(session).Get(TestCountryId);
			var reposititory = new CustomerRepository(session);
			var subscriptionRepository = new SubscriptionRepository(session);
			var transactionRepository = new TransactionRepository(session);
			var errorRepository = new SubscriptionErrorRepository(session);
			for (var i = 0; i < 20; i++)
			{
				var shop = (i % 3 == 0) ? shop1 : shop2;
				var customerToSave = CustomerFactory.Get(country, shop, "Tore " + i, "Alm " + i, "19630610613" + (i%9));
				reposititory.Save(customerToSave);
				var subscriptionToSave = SubscriptionFactory.Get(customerToSave, ((i % 3) +1).ToEnum<SubscriptionStatus>());
				subscriptionRepository.Save(subscriptionToSave);
				TransactionFactory.GetList(subscriptionToSave).Each(transactionRepository.Save);
				SubscriptionErrorFactory.GetList(subscriptionToSave).Each(errorRepository.Save);
			}
		}

		protected ISessionFactory GetSessionFactory()
		{
			return NHibernateFactory.Instance.GetSessionFactory();
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}
	}
}