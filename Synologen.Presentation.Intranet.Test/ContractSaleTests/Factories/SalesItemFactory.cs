using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class SalesItemFactory
	{
		public static IEnumerable<SaleItem> GetList(ContractCompany contractCompany)
		{
			var article1 = ArticleFactory.Get(1, contractCompany.ContractId);
			var article2 = ArticleFactory.Get(2, contractCompany.ContractId);
			var article3 = ArticleFactory.Get(3, contractCompany.ContractId);
			var contractSale1 = ContractSaleFactory.Get(23, contractCompany);
			var contractSale2 = ContractSaleFactory.Get(24, contractCompany);
			return new[]
			{
				new SaleItem{Article = article1, Quantity = 1, Sale = contractSale1, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = false},
				new SaleItem{Article = article2, Quantity = 2, Sale = contractSale1, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = false},
				new SaleItem{Article = article3, Quantity = 3, Sale = contractSale1, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = true},
				new SaleItem{Article = article1, Quantity = 4, Sale = contractSale2, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = false},
				new SaleItem{Article = article2, Quantity = 5, Sale = contractSale2, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = false},
				new SaleItem{Article = article3, Quantity = 6, Sale = contractSale2, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = true},
				new SaleItem{Article = article1, Quantity = 7, Sale = contractSale2, SingleItemPriceExcludingVAT = 55.55M, IsVATFree = false},
			};
		}
	}
}