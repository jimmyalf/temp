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
			const int testableShopId = 158;
			const string expectedName = "Testbutik för bågbeställning";
			const string expectedAddressLineOne = "Spinit AB";
			const string expectedAddressLineTwo = "Datavägen 2";
			const string expectedPostalCode = "43632";
			const string expectedCity = "Askim";

			//Act
			var persistedShop = ShopRepository.Get(testableShopId);

			//Assert
			Expect(persistedShop, Is.Not.Null);
			Expect(persistedShop.Id, Is.EqualTo(testableShopId));
			Expect(persistedShop.Name, Is.EqualTo(expectedName));
			Expect(persistedShop.Address.AddressLineOne, Is.EqualTo(expectedAddressLineOne));
			Expect(persistedShop.Address.AddressLineTwo, Is.EqualTo(expectedAddressLineTwo));
			Expect(persistedShop.Address.PostalCode, Is.EqualTo(expectedPostalCode));
			Expect(persistedShop.Address.City, Is.EqualTo(expectedCity));
		}
		
	}
}