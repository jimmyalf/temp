using System;
using System.Configuration;
using System.Data.SqlClient;
using NHibernate;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ContractSales;
using Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Utility.Business;
using Shop=Spinit.Wpc.Synologen.Business.Domain.Entities.Shop;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	public class BaseRepositoryTester<TModel> :  NHibernateRepositoryTester<TModel> //: AssertionHelper
	{
		protected SqlProvider Provider;
		protected Shop TestShop;
		const int testableShopId = 158;
		public const int TestableShopMemberId = 485;
		public const int TestableCompanyId = 57;
		const string connectionStringname = "WpcServer";

		protected override Action SetUp()
		{
			return () =>
			{
				Provider = new SqlProvider(ConnectionString);
				SetupData();
				ActionCriteriaExtensions.ConstructConvertersUsing(ResolveCriteriaConverters);
			};
		}

		private void SetupData() 
		{
			if(String.IsNullOrEmpty(DataHelper.ConnectionString)){
				throw new OperationCanceledException("Connectionstring could not be found in configuration");
			}
			if(!IsDevelopmentServer(DataHelper.ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
			var sqlConnection = new SqlConnection(DataHelper.ConnectionString);

			sqlConnection.Open();
			
			DataHelper.DeleteForTable(sqlConnection, "tblSynologenSettlementOrderConnection");
			DataHelper.DeleteAndResetIndexForTable(sqlConnection, "tblSynologenSettlement");
			DataHelper.DeleteAndResetIndexForTable(sqlConnection, "tblSynologenOrderHistory");
			DataHelper.DeleteAndResetIndexForTable(sqlConnection, "tblSynologenOrderItems");
			DataHelper.DeleteAndResetIndexForTable(sqlConnection, "tblSynologenOrder");
			sqlConnection.Close();

			SetupTestData();
		}

		private void SetupTestData() 
		{
			TestShop = Factories.ShopFactory.GetShop(testableShopId, ShopAccess.None);
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref TestShop);
		}

		private static string ConnectionString { get { return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString; } }

		[TearDown]
		public void ResetTestData()
		{
			TestShop = Factories.ShopFactory.GetShop(testableShopId, ShopAccess.LensSubscription | ShopAccess.SlimJim);
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref TestShop);
		}

		protected override ISessionFactory GetSessionFactory() 
		{ 
			return NHibernateFactory.Instance.GetSessionFactory();
		}

		private object ResolveCriteriaConverters<TType>(TType objectToResolve)
		{

			if (objectToResolve.Equals(typeof(IActionCriteriaConverter<AllContractSalesMatchingCriteria, ICriteria>)))
			{
				return new AllContractSalesMatchingCriteriaConverter(GetSessionFactory().OpenSession());
			}

			throw new ArgumentException(String.Format("No criteria converter has been defined for {0}", objectToResolve), "objectToResolve");
		}

		protected virtual bool IsDevelopmentServer(string connectionString)
		{
			if(connectionString.ToLower().Contains("black")) return true;
			if(connectionString.ToLower().Contains("localhost")) return true;
			return false;
		}
	}
}