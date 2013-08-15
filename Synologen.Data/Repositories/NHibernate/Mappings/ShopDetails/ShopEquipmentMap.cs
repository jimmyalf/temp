using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ShopDetails
{
    public class ShopEquipmentMap : ClassMap<ShopEquipment>
    {
        public ShopEquipmentMap()
        {
            Table("tblSynologenShopEquipment");
            Id(x => x.Id).Column("cId");
            Map(x => x.Name).Column("cName");

            HasMany(x => x.Connections).Table("tblSynologenShopEquipmentConnection").ReadOnly().NotFound.Ignore();
        }
    }
}
