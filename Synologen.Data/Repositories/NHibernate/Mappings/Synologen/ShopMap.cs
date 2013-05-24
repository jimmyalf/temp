using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Synologen;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Synologen
{
	public class ShopMap : ClassMap<Shop>
	{
		public ShopMap()
		{
			Table("tblSynologenShop");
			Id(x => x.Id).Column("cId");
			Map(x => x.Name).Column("cShopName");
			Map(x => x.Number).Column("cShopNumber");
			References(x => x.ShopGroup).Column("cShopGroupId");
		}
	 
	}
}