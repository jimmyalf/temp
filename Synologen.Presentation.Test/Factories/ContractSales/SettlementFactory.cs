using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class SettlementFactory
	{
		public static ShopSettlement Get(int id)
		{
			var settlementMock = new Mock<ShopSettlement>();
			settlementMock.SetupGet(x => x.Id).Returns(id);
			settlementMock.SetupGet(x => x.CreatedDate).Returns(new DateTime(2010, 11, 09));
			var shop1 = ShopFactory.GetShop(1);
			var shop2 = ShopFactory.GetShop(2);
			var shop3 = ShopFactory.GetShop(50);
			settlementMock.SetupGet(x => x.ContractSales).Returns( new[]
            {
				ContractSaleFactory.GetContractSale(3, shop2, 123.45M),
				ContractSaleFactory.GetContractSale(4, shop2, 234.56M),
				ContractSaleFactory.GetContractSale(1, shop1, 345.67M),
				ContractSaleFactory.GetContractSale(5, shop2, 456.78M),
				ContractSaleFactory.GetContractSale(2, shop1, 567.89M),
				ContractSaleFactory.GetContractSale(6, shop3, 678.90M),
			});
			var subscription1 = SubscriptionFactory.Get(1, 1, 1);
			var subscription2 = SubscriptionFactory.Get(2, 2, 2);
			var subscription3 = SubscriptionFactory.Get(3, 3, 100);
			settlementMock.SetupGet(x => x.LensSubscriptionTransactions).Returns(new []
			{
				TransactionFactory.Get(1, 285.45M, subscription1),
				TransactionFactory.Get(2, 12.86M, subscription1),
				TransactionFactory.Get(3, 775, subscription2),
				TransactionFactory.Get(4, 256, subscription1),
				TransactionFactory.Get(5, 125, subscription1),
				TransactionFactory.Get(6, 555.55M, subscription2),
				TransactionFactory.Get(7, 129, subscription3),
			});
			return settlementMock.Object;
		}

		public static IEnumerable<ShopSettlement> GetList() 
		{
			return GetList(15);
		}
		public static IEnumerable<ShopSettlement> GetList(int numberOfItems) 
		{
			return Enumerable.Range(0, numberOfItems).Select(index => Get(index));
		}
	}
}
