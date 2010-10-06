using System.Configuration;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Test.ContractSales
{
	public class TestBase : AssertionHelper
	{
		protected SqlProvider Provider;
		protected Shop TestShop;
		const int testableShopId = 158;
		const string connectionStringname = "WpcServer";


		public void SetupDefaultContext()
		{
			Provider = new SqlProvider(ConnectionString);
			SetupTestShop();
		}

		private void SetupTestShop() 
		{
			TestShop = Factories.ShopFactory.GetShop(testableShopId);
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref TestShop);
		}

		private static string ConnectionString { get { return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString; } }

	}
}