using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails
{
    public class Shop
    {
        public virtual int Id { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual ShopAddress Address { get; set; }
        public virtual Coordinates Coordinates { get; set; }
        public virtual string Description { get; set; }
        public virtual string Url { get; set; }
        public virtual string MapUrl { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }

        public virtual IEnumerable<ShopEquipmentConnection> Connections { get; set; }
    }
}
