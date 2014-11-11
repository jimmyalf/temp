using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Synologen;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class ShopGroupFormView : CommonFormView
	{
		public ShopGroupFormView()
		{
			Shops = Enumerable.Empty<ShopGroupShopListItem>();
		}

		public ShopGroupFormView(ShopGroup shopGroup) : base(shopGroup.Id)
		{
			Name = shopGroup.Name;
			Shops = shopGroup.Shops.OrEmpty().Select(x => new ShopGroupShopListItem(x));
		}

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		public IEnumerable<ShopGroupShopListItem> Shops { get; set; }
	}

	public class ShopGroupShopListItem
	{
		public ShopGroupShopListItem() { }
		public ShopGroupShopListItem(Shop shop)
		{
			Id = shop.Id;
			Name = shop.Name;
			Number = shop.Number;
		}

		public string Number { get; set; }
		public string Name { get; set; }
		public int Id { get; set; }
	}
}