using System;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Core.Dependencies.NHibernate;
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
		//protected int TestShop2Id = 159;

		[SetUp]
		public void RunBeforeAnyTests()
		{
			//Setup NHibernate
            if(!NHibernateFactory.MappingAssemblies.Any())
			{
				var assembly = typeof(Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions.SubscriptionMap).Assembly;
				NHibernateFactory.MappingAssemblies.Add(assembly);				
			}
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
			//const long testInvoiceNumber = 1865;
			const int testableShopId = 158;
			const int TestableShopMemberId = 485;
			const int TestableCompanyId = 57;
			var orders = new[]
			{
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
				OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId),
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
				OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId),
				OrderFactory.Get(TestableCompanyId, nonSettlementableOrderStatus, testableShopId, TestableShopMemberId),
				OrderFactory.Get(TestableCompanyId, settlementableOrderStatus, testableShopId, TestableShopMemberId),
			};
			orders.Each(order =>
			{
				provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
				provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
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
			var shop = new ShopRepository(session).Get(TestShopId);
			var country = new CountryRepository(session).Get(TestCountryId);
			var reposititory = new CustomerRepository(session);
			var subscriptionRepository = new SubscriptionRepository(session);
			var transactionRepository = new TransactionRepository(session);
			var errorRepository = new SubscriptionErrorRepository(session);
			for (var i = 0; i < 5; i++)
			{
				var customerToSave = CustomerFactory.Get(country, shop, "Tore " + i, "Alm " + i, "19630610613" + i);
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