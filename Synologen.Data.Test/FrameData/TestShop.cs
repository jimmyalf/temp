using NUnit.Framework;
using Spinit.Wpc.Synologen.Integration.Data.Test.FrameData;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData
{
	[TestFixture, Category("TestShop")]
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
			//Arrange
			//const int testableShopId = 158;
			var shop = CreateShop(GetNewSession());
			//const string expectedName = "Testbutik för bågbeställning";
			//const string expectedAddressLineOne = "Spinit AB";
			//const string expectedAddressLineTwo = "Datavägen 2";
			//const string expectedPostalCode = "43632";
			//const string expectedCity = "Askim";

			//Act
			var persistedShop = ShopRepository.Get(shop.Id);

			//Assert
			Expect(persistedShop, Is.Not.Null);
			Expect(persistedShop.Id, Is.EqualTo(shop.Id));
			Expect(persistedShop.Name, Is.EqualTo(shop.Name));
			Expect(persistedShop.Address.AddressLineOne, Is.EqualTo(shop.Address.AddressLineOne));
			Expect(persistedShop.Address.AddressLineTwo, Is.EqualTo(shop.Address.AddressLineTwo));
			Expect(persistedShop.Address.PostalCode, Is.EqualTo(shop.Address.PostalCode));
			Expect(persistedShop.Address.City, Is.EqualTo(shop.Address.City));
		}
		
	}
}