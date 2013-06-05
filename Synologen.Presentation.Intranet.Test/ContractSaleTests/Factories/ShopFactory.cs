using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class ShopFactory
	{
		public static Shop Get(int id)
		{
			var mockedShop = new Mock<Shop>();
			mockedShop.SetupGet(x => x.Id).Returns(id);
			mockedShop.SetupGet(x => x.Name).Returns("Butik");
			mockedShop.SetupGet(x => x.Number).Returns("1234");
			return mockedShop.Object;
		}
	}
}