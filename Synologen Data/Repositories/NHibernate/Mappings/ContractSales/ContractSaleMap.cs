using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class ContractSaleMap : ClassMap<ContractSale>
	{
		public ContractSaleMap()
		{
			Table("tblSynologenOrder");
			Id(x => x.Id).Column("cId");
			Map(x => x.TotalAmountExcludingVAT).Column("cInvoiceSumExcludingVAT");
			Map(x => x.TotalAmountIncludingVAT).Column("cInvoiceSumIncludingVAT");
			References(x => x.Shop).Column("cSalesPersonShopId");
			Map(x => x.StatusId).Column("cStatusId");
		}
	}
}