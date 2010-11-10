using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales
{
	[TestFixture]
	public class Given_a_persisted_shop : BaseRepositoryTester <SettlementRepository>
	{
		
		public Given_a_persisted_shop()
		{
			Context = session => SetupDefaultContext();
			Because = session => { };
		}

		[Test]
		public void Can_get_persisted_shop()
		{
			//Act
			var persistedShop = Provider.GetShop(TestShop.ShopId);

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
			var editedShop = Factories.ShopFactory.GetShop(TestShop.ShopId, ShopAccess.LensSubscription | ShopAccess.SlimJim);

			//Act
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref editedShop);
			var fetchedShop = Provider.GetShop(TestShop.ShopId);

			//Assert
			fetchedShop.Access.HasOption(ShopAccess.LensSubscription).ShouldBe(true);
			fetchedShop.Access.HasOption(ShopAccess.SlimJim).ShouldBe(true);
		}
		
	}
}
