using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ShopDetails
{
    public class ShopEquipmentConnectionMap : ClassMap<ShopEquipmentConnection>
    {
        public ShopEquipmentConnectionMap()
        {
            Table("tblSynologenShopEquipmentConnection");
            Id(x => x.Id).Column("cId");

            References(x => x.Shop).ReadOnly().Column("cShopId");
            References(x => x.ShopEquipment).ReadOnly().Column("cShopEquipmentId");
        }
    }
}
