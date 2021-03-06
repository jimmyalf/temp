using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales.Factories
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
				CompanyUnit = "1234",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				PersonalIdNumber = "197010245111",
                CustomerOrderNumber = "987654",                
			};	
		}
		public static Order Get(int companyId, int settlementableOrderStatus, int shopId, int memberId, int articleId) 
		{
			var order = Get(companyId, settlementableOrderStatus, shopId, memberId);
			order.OrderItems = new List<OrderItem>
			{
				GetOrderItem(articleId, 0),
				GetOrderItem(articleId, 0),
				GetOrderItem(articleId, 0),
				GetOrderItem(articleId, 0),
				GetOrderItem(articleId, 0),
			};
			order.InvoiceSumExcludingVAT = order.OrderItems.Sum(x => x.DisplayTotalPrice);
			order.InvoiceSumIncludingVAT = order.InvoiceSumExcludingVAT * 1.25F;
			return order;
		}

		public static OrderItem GetOrderItem(int articleId, int orderId)
		{
			return new OrderItem
			{
				ArticleDisplayName = "Artikel A",
				ArticleDisplayNumber = "123",
				ArticleId = articleId,
				DisplayTotalPrice = 999.75F,
				Notes = "Anteckningar",
				NoVAT = false,
				NumberOfItems = 3,
				SinglePrice = 333.25F,
				OrderId = orderId,
				SPCSAccountNumber = "1234",
			};
		}
	}
}