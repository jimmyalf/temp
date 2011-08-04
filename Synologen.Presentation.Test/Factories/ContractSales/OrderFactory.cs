using FakeItEasy;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class OrderFactory 
	{
		public static Order GetInvoicedOrder()
		{
			return new Order
			{
				Id = 5,
				PersonalIdNumber = "198512242101",
				CompanyUnit = "1030",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				StatusId = 5 /* Invoiced Status */,
				Phone = "031 - 12 34 56",
				Email = "adam.bertil@testbolaget.se",
				SalesPersonMemberId = 7,
				SalesPersonShopId = 8,
				RstText = "1234",
				CustomerOrderNumber = "ABC123",
				CompanyId = 6,
				InvoiceNumber = 12345
			};
		}

		public static OrderStatus GetOrderStatus()
		{
			return new OrderStatus
			{
				Id = 2, 
				Name = "Order-status ABC", 
				OrderNumber = 3
			};
		}

		public static Order GetUnInvoicedOrder()
		{
			var order = GetInvoicedOrder();
			order.StatusId = 1; /* Order created status */
			return order;
		}

		public static WpcUser GetUser()
		{
			var user = A.Fake<IBaseUserRow>();
			A.CallTo(() => user.UserName).Returns("AnvändareABC");
			return new WpcUser {User = user};
		}
	}
}