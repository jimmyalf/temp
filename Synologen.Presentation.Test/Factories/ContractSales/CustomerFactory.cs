using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class CustomerFactory
	{
		public static OldCustomer Get(int id, Shop shop)
		{
			var mockedCustomer = new Mock<OldCustomer>();
			mockedCustomer.SetupGet(x => x.Shop).Returns(shop);
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			return mockedCustomer.Object;
		}
		public static OldCustomer Get(int id, int shopId)
		{
			return Get(id, ShopFactory.GetShop(shopId));
		}
	}
}