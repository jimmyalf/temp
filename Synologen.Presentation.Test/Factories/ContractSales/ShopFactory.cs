using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public class ShopFactory
	{
		public static Shop GetShop(int id)
		{
			var shopMock = new Mock<Shop>();
			shopMock.SetupGet(x => x.Id).Returns(id);
			shopMock.SetupGet(x => x.BankGiroNumber).Returns("123456987");
			shopMock.SetupGet(x => x.Name).Returns("Örebro Optik");
			shopMock.SetupGet(x => x.Number).Returns("1350");
			return shopMock.Object;
		}			
	}
}