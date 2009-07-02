using Spinit.Wpc.Synologen.Business.Interfaces;
namespace Spinit.Wpc.Synologen.Data.Types {
	public class ShopEquipmentRow : IShopEquipmentRow {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}