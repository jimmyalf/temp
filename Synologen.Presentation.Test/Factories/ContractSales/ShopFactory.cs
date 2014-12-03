using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class ShopFactory
	{
		public static Shop GetShop(int id, string shopName = "Örebro Optik", string shopNumber = "1350")
		{
			var shopMock = new Mock<Shop>();
			shopMock.SetupGet(x => x.Id).Returns(id);
			shopMock.SetupGet(x => x.BankGiroNumber).Returns("123456987");
			shopMock.SetupGet(x => x.Name).Returns(shopName);
			shopMock.SetupGet(x => x.Number).Returns(shopNumber);
			return shopMock.Object;
		}			
	}
}