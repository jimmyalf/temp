namespace Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails
{
    public class ShopEquipmentConnection
    {
        public virtual int Id { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ShopEquipment ShopEquipment { get; set; }
    }
}
