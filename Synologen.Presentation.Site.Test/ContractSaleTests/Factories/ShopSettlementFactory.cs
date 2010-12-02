using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.Factories
{
	public static class ShopSettlementFactory 
	{
		public static ShopSettlement Get(int id) 
		{ 
			return	new ShopSettlement
			{
				Id = 50,
				CreatedDate = new DateTime(2010,12,01),
				LensSubscriptionTransactions = TransactionFactory.GetList(),
				SaleItems = SalesItemFactory.GetList(ContractCompanyFactory.Get()),
				Shop = ShopFactory.Get(55),
                ContractSalesValueIncludingVAT = 1234.56M
			};
		}
	}
}