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
			Map(x => x.TotalAmountIncludingVAT).Column("cInvoiceSumIncludingVAT");
			References(x => x.Shop)
				.Column("cSalesPersonShopId")
				.Fetch.Join();
			Map(x => x.StatusId).Column("cStatusId");
			HasMany(x => x.SaleItems)
				.KeyColumn("cOrderId");
			References(x => x.ContractCompany)
				.Column("cCompanyId")
				.Fetch.Join();
		}
	}
}