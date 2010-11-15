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
			settlementMock.SetupGet(x => x.ContractSales).Returns( new[]
            {
				ContractSaleFactory.GetContractSale(3, ShopFactory.GetShop(2)),
				ContractSaleFactory.GetContractSale(4, ShopFactory.GetShop(2)),
				ContractSaleFactory.GetContractSale(1, ShopFactory.GetShop(1)),
				ContractSaleFactory.GetContractSale(5, ShopFactory.GetShop(2)),
				ContractSaleFactory.GetContractSale(2, ShopFactory.GetShop(1)),
			});
			settlementMock.SetupGet(x => x.LensSubscriptionTransactions).Returns(TransactionFactory.GetList());
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
