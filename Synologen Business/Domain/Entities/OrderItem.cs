using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract]
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
		[DataMember] public int Id { get; set; }
		[DataMember] public int ArticleId { get; set; }
		[DataMember] public string ArticleDisplayName { get; set; }
		[DataMember] public float SinglePrice { get; set; }
		[DataMember] public int NumberOfItems { get; set; }
		[DataMember] public string Notes { get; set; }
		[DataMember] public string ArticleDisplayNumber { get; set; }
		[DataMember] public float DisplayTotalPrice { get; set; }
		[DataMember] public int OrderId { get; set; }
		[DataMember] public bool NoVAT { get; set; }
		[DataMember] public string SPCSAccountNumber { get; set; }
	}
}