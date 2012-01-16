using System.Collections.Generic;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class OrderListView : CommonListView<OrderListItem, Core.Domain.Model.Orders.Order>
	{
		public OrderListView() { }

		public OrderListView(string searchTerm, IEnumerable<Core.Domain.Model.Orders.Order> orders) 
			: base(orders, searchTerm) { } 

		public override OrderListItem Convert(Core.Domain.Model.Orders.Order order)
		{
			return new OrderListItem
			{
				CreatedDate = order.Created.ToString("yyyy-MM-dd"),
				CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName),
				OrderId = order.Id.ToString(),
				ShopName = null
			};
		}
	}

	public class OrderListItem
	{
		public string OrderId { get; set; }
		public string ShopName { get; set; }
		public string CustomerName { get; set; }
		public string CreatedDate { get; set; }
	}
}