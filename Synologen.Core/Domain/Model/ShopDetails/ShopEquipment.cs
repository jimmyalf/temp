using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails
{
    public class ShopEquipment
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<ShopEquipmentConnection> Connections { get; set; }
    }
}
