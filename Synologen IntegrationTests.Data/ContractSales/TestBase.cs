using System.Configuration;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	public class TestBase : AssertionHelper
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
			TestShop = Factories.ShopFactory.GetShop(testableShopId);
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref TestShop);
		}

		private static string ConnectionString { get { return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString; } }

	}
}