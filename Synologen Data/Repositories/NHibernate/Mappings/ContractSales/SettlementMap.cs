using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class SettlementMap : ClassMap<Settlement>
	{
		public SettlementMap()
		{
			Table("tblSynologenSettlement");
			Id(x => x.Id).Column("cId");
			Map(x => x.CreatedDate).Column("cCreatedDate");
			HasManyToMany(x => x.ContractSales)
				.Table("tblSynologenSettlementOrderConnection")
				.ParentKeyColumn("cSettlementId")
				.ChildKeyColumn("cOrderId");
			HasMany(x => x.LensSubscriptionTransactions)
				.KeyColumn("SettlementId");
		}
	}
}