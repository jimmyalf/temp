using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class SupplierListView : CommonListView<SupplierListItem, ArticleSupplier>
	{
		public SupplierListView() { }
		public SupplierListView(string search, IEnumerable<ArticleSupplier> suppliers) : base(suppliers,search) { }

		public override SupplierListItem Convert(ArticleSupplier item)
		{
			return new SupplierListItem
			{
				SupplierId = item.Id.ToString(), 
				Name = item.Name,
				OrderEmail = item.OrderEmailAddress,
				Active = item.Active
			};
		}
	}

	public class SupplierListItem
	{
		public string SupplierId { get; set; }
		public string Name { get; set; }
		public string OrderEmail { get; set; }
		public bool Active { get; set; }
	}
}