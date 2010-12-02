using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class SaleItemMap : ClassMap<SaleItem>
	{
		public SaleItemMap()
		{
			Table("tblSynologenOrderItems");
			Id(x => x.Id).Column("cId");
			References(x => x.Article).Column("cArticleId");
			Map(x => x.Quantity).Column("cNumberOfItems");
			Map(x => x.SingleItemPriceExcludingVAT).Column("cSinglePrice");
			References(x => x.Sale).Column("cOrderId");
			Map(x => x.IsVATFree).Column("cNoVAT");
		}
	}
}