using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class ContractSalesOrderFactory 
	{
		public static Order GetOrder(int companyId, int salesPersonMemberId, int shopId, int statusId)
		{
			return new Order
			{
				CompanyId = companyId,
				CreatedDate = new DateTime(2011,01,01),
				CompanyUnit = "Unit",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				CustomerOrderNumber = null,
				Email = "a.b@test.se",
				InvoiceNumber = 1234,
				InvoiceSumExcludingVAT = 256,
				InvoiceSumIncludingVAT = 302,
				MarkedAsPayedByShop = false,
				OrderItems = null,
				PersonalIdNumber = "197001015566",
				Phone = "031-0123456",
				RstText = "RST",
				SalesPersonMemberId = salesPersonMemberId,
				SalesPersonShopId = shopId,
				StatusId = statusId,
			};
		}

        public static OrderItem GetOrderItem(int orderId, ContractArticleConnection connection, int quantity = 1)
        {
            return new OrderItem
            {
                ArticleDisplayName = connection.ArticleDescription,
                ArticleDisplayNumber = connection.ArticleNumber,
                ArticleId = connection.ArticleId,
                DisplayTotalPrice = connection.Price * quantity,
                NoVAT = connection.NoVAT,
                NumberOfItems = quantity,
                OrderId = orderId,
                SinglePrice = connection.Price,
            };
        }

	    public static Article GetArticle()
	    {
	        return new Article
	        {
	            DefaultSPCSAccountNumber = "123",
	            Description = "Testartikel abc",
	            Name = "Testartikel",
	            Number = "123-456"
	        };
	    }
	}
}