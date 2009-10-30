using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class ShopEquipment : IShopEquipment {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}