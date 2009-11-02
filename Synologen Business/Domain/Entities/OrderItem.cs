using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class OrderItem : IOrderItem {

		public OrderItem(IOrderItem orderItem) {
			Id = orderItem.Id;
			ArticleId = orderItem.ArticleId;
			ArticleDisplayName = orderItem.ArticleDisplayName;
			SinglePrice = orderItem.SinglePrice;
			NumberOfItems = orderItem.NumberOfItems;
			Notes = orderItem.Notes;
			ArticleDisplayNumber = orderItem.ArticleDisplayNumber;
			DisplayTotalPrice = orderItem.DisplayTotalPrice;
			OrderId = orderItem.OrderId;
			NoVAT = orderItem.NoVAT;
			SPCSAccountNumber = orderItem.SPCSAccountNumber;
		}
		public OrderItem() { }
		public int Id { get; set; }
		public int ArticleId { get; set; }
		public string ArticleDisplayName { get; set; }
		public float SinglePrice { get; set; }
		public int NumberOfItems { get; set; }
		public string Notes { get; set; }
		public string ArticleDisplayNumber { get; set; }
		public float DisplayTotalPrice { get; set; }
		public int OrderId { get; set; }
		public bool NoVAT { get; set; }
		public string SPCSAccountNumber { get; set; }
	}
}