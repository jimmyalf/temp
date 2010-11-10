using System;
using System.Configuration;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Data;
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


		public void SetupDefaultContext()
		{
			Provider = new SqlProvider(ConnectionString);
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
	}
}