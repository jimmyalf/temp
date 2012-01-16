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
		}
	}
}