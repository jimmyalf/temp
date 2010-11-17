using System.Collections.Generic;
using System.Linq;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public class ContractSaleFactory
	{
		public static ContractSale GetContractSale(int id, Shop shop)
		{
			return GetContractSale(id, shop, 26956.53M);
		}
		
		public static ContractSale GetContractSale(int id, Shop shop, decimal amount)
		{
			var contractSaleMock = new Mock<ContractSale>();
			contractSaleMock.SetupGet(x => x.Id).Returns(id);
			contractSaleMock.SetupGet(x => x.Shop).Returns(shop);
			contractSaleMock.SetupGet(x => x.TotalAmountIncludingVAT).Returns(amount);
			return contractSaleMock.Object;
		}

		public static IEnumerable<ContractSale> GetList(int numberOfItems) 
		{
			return Enumerable.Range(1, numberOfItems).Select(index => GetContractSale(index, ShopFactory.GetShop(index)));
		}
	}
}