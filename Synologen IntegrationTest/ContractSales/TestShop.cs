using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Integration.Test.ContractSales
{
	[TestFixture]
	public class Given_a_persisted_shop : TestBase 
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_persisted_shop()
		{
			//Act
			var persistedShop = Provider.GetShop(TestShop.ShopId);

			//Assert
			Expect(persistedShop.Access, Is.EqualTo(TestShop.Access));
			Expect(persistedShop.Active, Is.EqualTo(TestShop.Active));
			Expect(persistedShop.Address, Is.EqualTo(TestShop.Address));
			Expect(persistedShop.Address2, Is.EqualTo(TestShop.Address2));
			Expect(persistedShop.CategoryId, Is.EqualTo(TestShop.CategoryId));
			Expect(persistedShop.City, Is.EqualTo(TestShop.City));
			Expect(persistedShop.Concern, Is.EqualTo(TestShop.Concern));
			Expect(persistedShop.ContactFirstName, Is.EqualTo(TestShop.ContactFirstName));
			Expect(persistedShop.ContactLastName, Is.EqualTo(TestShop.ContactLastName));
			Expect(persistedShop.Description, Is.EqualTo(TestShop.Description));
			Expect(persistedShop.Email, Is.EqualTo(TestShop.Email));
			Expect(persistedShop.Fax, Is.EqualTo(TestShop.Fax));
			Expect(persistedShop.GiroId, Is.EqualTo(TestShop.GiroId));
			Expect(persistedShop.GiroNumber, Is.EqualTo(TestShop.GiroNumber));
			Expect(persistedShop.GiroSupplier, Is.EqualTo(TestShop.GiroSupplier));
			Expect(persistedShop.HasConcern, Is.EqualTo(TestShop.HasConcern));
			Expect(persistedShop.MapUrl, Is.EqualTo(TestShop.MapUrl));
			Expect(persistedShop.Name, Is.EqualTo(TestShop.Name));
			Expect(persistedShop.Number, Is.EqualTo(TestShop.Number));
			Expect(persistedShop.Phone, Is.EqualTo(TestShop.Phone));
			Expect(persistedShop.Phone2, Is.EqualTo(TestShop.Phone2));
			Expect(persistedShop.ShopId, Is.EqualTo(TestShop.ShopId));
			Expect(persistedShop.Url, Is.EqualTo(TestShop.Url));
			Expect(persistedShop.Zip, Is.EqualTo(TestShop.Zip));
		}

		[Test]
		public void Can_update_shop_Access()
		{
			//Arrange
			var editedShop = Factories.ShopFactory.GetShop(TestShop.ShopId);
			editedShop.Access = ShopAccess.LensSubscription | ShopAccess.SlimJim;

			//Act
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref editedShop);
			var fetchedShop = Provider.GetShop(TestShop.ShopId);

			//Assert
			Expect(fetchedShop.Access.HasOption(ShopAccess.LensSubscription), Is.True);
			Expect(fetchedShop.Access.HasOption(ShopAccess.SlimJim), Is.True);
		}
		
	}
}
