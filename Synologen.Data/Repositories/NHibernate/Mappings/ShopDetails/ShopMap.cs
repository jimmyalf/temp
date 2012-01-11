using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ShopDetails
{
    public class ShopMap : ClassMap<Shop>
    {
        public ShopMap()
        {
            Table("tblSynologenShop");
            Id(x => x.Id).Column("cId");

            Map(x => x.Active).Column("cActive");
            Map(x => x.Name).Column("cShopName");
            Map(x => x.Description).Column("cShopDescription");
            Map(x => x.Email).Column("cEmail");
            Map(x => x.MapUrl).Column("cMapUrl");
            Map(x => x.Url).Column("cUrl");
            Map(x => x.Phone).Column("cPhone");

            Component(x => x.Address, m =>
            {
                m.Map(x => x.AddressLineOne).Column("cAddress");
                m.Map(x => x.AddressLineTwo).Column("cAddress2");
                m.Map(x => x.City).Column("cCity");
                m.Map(x => x.PostalCode).Column("cZip");
            });
            Component(x => x.Coordinates, m =>
            {
                m.Map(x => x.Latitude).Column("cLatitude");
                m.Map(x => x.Longitude).Column("cLongitude");
            });

            HasMany(x => x.Connections).ReadOnly().Table("tblSynologenShopEquipmentConnection").NotFound.Ignore();
        }
    }
}
