using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.ContractSales.Factories
{
	public static class OrderFactory 
	{
		public static Order Get(int companyId, int settlementableOrderStatus, int shopId, int memberId ) 
		{
			return new Order
			{
				CompanyId = companyId,
				InvoiceSumIncludingVAT = 556.23,
				InvoiceSumExcludingVAT = 495,
				StatusId = settlementableOrderStatus,
				SalesPersonShopId = shopId,
				SalesPersonMemberId = memberId,
			};	
		}
		public static Order Get(int companyId, int settlementableOrderStatus, int shopId, int memberId, long? invoiceNumber) 
		{
			return new Order
			{
				CompanyId = companyId,
				InvoiceSumIncludingVAT = 556.23,
				InvoiceSumExcludingVAT = 495,
				StatusId = settlementableOrderStatus,
				SalesPersonShopId = shopId,
				SalesPersonMemberId = memberId,
				InvoiceNumber = invoiceNumber ?? 0
			};	
		}
	}
}