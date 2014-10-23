using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class ShopMap : ClassMap<Shop>
	{
		public ShopMap()
		{
			Table("tblSynologenShop");
			Id(x => x.Id).Column("cId");
			Map(x => x.BankGiroNumber).Column("cGiroNumber");
			Map(x => x.Name).Column("cShopName");
			Map(x => x.Number).Column("cShopNumber");
		}
	}
}