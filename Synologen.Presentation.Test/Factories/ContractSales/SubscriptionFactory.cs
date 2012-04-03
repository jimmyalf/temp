using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class SubscriptionFactory
	{
		public static OldSubscription GetOld(int id, Customer customer)
		{
			var mockedSubscription = new Mock<OldSubscription>();
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			return mockedSubscription.Object;
		}
		public static OldSubscription GetOld(int id, int customerId, int shopId)
		{
			return GetOld(id, CustomerFactory.Get(customerId, shopId));
		}
		public static OldSubscription GetOld(int id, int customerId, Shop shop)
		{
			return GetOld(id, CustomerFactory.Get(customerId, shop));
		}

		public static NewSubscription GetNew(int id, Shop shop = null)
		{
			var shopToUse = shop ?? ShopFactory.GetShop(5);
			var mockedSubscription = new Mock<NewSubscription>();
			mockedSubscription.SetupGet(x => x.Shop).Returns(shopToUse);
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			return mockedSubscription.Object;
		}
		public static NewSubscription GetNew(int id, int shopId)
		{
			var shop = ShopFactory.GetShop(shopId);
			return GetNew(id, shop);
		}
	}
}