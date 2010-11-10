using System;
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
			return settlementMock.Object;
		}
	}

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

	public class ContractSaleFactory
	{
		public static ContractSale GetContractSale(int id, Shop shop)
		{
			var contractSaleMock = new Mock<ContractSale>();
			contractSaleMock.SetupGet(x => x.Id).Returns(id);
			contractSaleMock.SetupGet(x => x.Shop).Returns(shop);
			contractSaleMock.SetupGet(x => x.TotalAmountExcludingVAT).Returns(18956.23M);
			contractSaleMock.SetupGet(x => x.TotalAmountIncludingVAT).Returns(26956.53M);
			return contractSaleMock.Object;
		}
	}
}
