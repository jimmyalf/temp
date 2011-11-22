using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Utility.Business;
using Shop=Spinit.Wpc.Synologen.Business.Domain.Entities.Shop;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales
{
	[TestFixture, Category("TestSqlProviderForShop")]
	public class Given_a_persisted_shop : ContractSaleTestbase
	{
		private Shop persistedShop;
		private Shop TestShop;
		//protected const int testableShopId = 158;
		//protected const int testableShopId2 = 159;
		//public const int TestableShopMemberId = 485;
		//public const int TestableShop2MemberId = 484;
		//public const int TestableCompanyId = 57;
		//public const int TestableContractId = 14;

		public Given_a_persisted_shop()
		{
			Context = () =>
			{
				TestShop = CreateShop();
				//ShopFactory.GetShop(testableShopId, ShopAccess.None);				
			};
			Because = provider => provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref TestShop);
		}

		[Test]
		public void Can_get_persisted_shop()
		{
			//Act
			persistedShop = GetTestEntity().GetShop(TestShop.ShopId);

			//Assert
			persistedShop.Access.ShouldBe(TestShop.Access);
			persistedShop.Active.ShouldBe(TestShop.Active);
			persistedShop.Address.ShouldBe(TestShop.Address);
			persistedShop.Address2.ShouldBe(TestShop.Address2);
			persistedShop.CategoryId.ShouldBe(TestShop.CategoryId);
			persistedShop.City.ShouldBe(TestShop.City);
			persistedShop.Concern.ShouldBe(TestShop.Concern);
			persistedShop.ContactFirstName.ShouldBe(TestShop.ContactFirstName);
			persistedShop.ContactLastName.ShouldBe(TestShop.ContactLastName);
			persistedShop.Description.ShouldBe(TestShop.Description);
			persistedShop.Email.ShouldBe(TestShop.Email);
			persistedShop.Fax.ShouldBe(TestShop.Fax);
			persistedShop.GiroId.ShouldBe(TestShop.GiroId);
			persistedShop.GiroNumber.ShouldBe(TestShop.GiroNumber);
			persistedShop.GiroSupplier.ShouldBe(TestShop.GiroSupplier);
			persistedShop.HasConcern.ShouldBe(TestShop.HasConcern);
			persistedShop.MapUrl.ShouldBe(TestShop.MapUrl);
			persistedShop.Name.ShouldBe(TestShop.Name);
			persistedShop.Number.ShouldBe(TestShop.Number);
			persistedShop.Phone.ShouldBe(TestShop.Phone);
			persistedShop.Phone2.ShouldBe(TestShop.Phone2);
			persistedShop.ShopId.ShouldBe(TestShop.ShopId);
			persistedShop.Url.ShouldBe(TestShop.Url);
			persistedShop.Zip.ShouldBe(TestShop.Zip);
		}

		[Test]
		public void Can_update_shop_Access()
		{
			//Arrange
			var editedShop = ShopFactory.GetShop(TestShop.ShopId, ShopAccess.LensSubscription | ShopAccess.SlimJim);

			//Act
			GetTestEntity().AddUpdateDeleteShop(Enumerations.Action.Update, ref editedShop);
			var fetchedShop = GetTestEntity().GetShop(TestShop.ShopId);

			//Assert
			fetchedShop.Access.HasOption(ShopAccess.LensSubscription).ShouldBe(true);
			fetchedShop.Access.HasOption(ShopAccess.SlimJim).ShouldBe(true);
		}

		protected override SqlProvider GetTestEntity()
		{
			return new SqlProvider(DataHelper.ConnectionString);
		}
	}

	public abstract class ContractSaleTestbase : BehaviorActionTestbase<SqlProvider>
	{
		private readonly SqlProvider _sqlProvider;

		protected ContractSaleTestbase()
		{
			_sqlProvider = new SqlProvider(DataHelper.ConnectionString);
		}

		protected override void SetUp()
		{
			var sqlConnection = new SqlConnection(DataHelper.ConnectionString);
			sqlConnection.Open();
			DataHelper.DeleteShopsAndConnections(sqlConnection);
			sqlConnection.Close();
		}

		protected Shop CreateShop()
		{
			return DataHelper.CreateShop(_sqlProvider, "Testbutik");
		}

	}
}