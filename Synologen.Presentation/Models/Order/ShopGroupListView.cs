using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Synologen;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class ShopGroupListView : CommonListView<ShopGroupListItem, ShopGroup>
	{
		public ShopGroupListView() { }
		public ShopGroupListView(IEnumerable<ShopGroup> shopGroups) : base(shopGroups) { }

		public override ShopGroupListItem Convert(ShopGroup item)
		{
			return new ShopGroupListItem
			{
				Id = item.Id,
				Name = item.Name,
				NumberOfShops = item.Shops.Count
			};
		}
	}

	public class ShopGroupListItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int NumberOfShops { get; set; }
	}
}