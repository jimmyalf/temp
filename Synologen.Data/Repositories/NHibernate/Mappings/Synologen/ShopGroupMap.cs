using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Synologen;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Synologen
{
	public class ShopGroupMap : ClassMap<ShopGroup>
	{
		public ShopGroupMap()
		{
			Table("tblSynologenShopGroup");
			Id(x => x.Id).Column("Id");
			Map(x => x.Name).Column("Name");
			HasMany(x => x.Shops).KeyColumn("cShopGroupId").Cascade.None();
		}
	}
}