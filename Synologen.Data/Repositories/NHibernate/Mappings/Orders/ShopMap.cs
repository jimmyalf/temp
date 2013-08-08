using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class ShopMap : ClassMap<Shop>
	{
		public ShopMap()
		{
			Table("tblSynologenShop");
			Id(x => x.Id).Column("cId");
		    Map(x => x.Name).Column("cShopName");
		    Map(x => x.AddressLineOne).Column("cAddress");
            Map(x => x.AddressLineTwo).Column("cAddress2");
            Map(x => x.PostalCode).Column("cZip");
            Map(x => x.City).Column("cCity");
			Map(x => x.ExternalAccessUsername).Column("cExternalAccessUsername");
			Map(x => x.ExternalAccessHashedPassword).Column("cExternalAccessHashedPassword");
		}
	}
}