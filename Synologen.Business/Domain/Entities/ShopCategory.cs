using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class ShopCategory : IShopCategory{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}