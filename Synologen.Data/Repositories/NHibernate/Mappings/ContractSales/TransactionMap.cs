using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class TransactionMap : ClassMap<Transaction>
	{
		public TransactionMap()
		{
			Table("SynologenLensSubscriptionTransaction");
			Id(x => x.Id);
			Map(x => x.Amount);
			Map(x => x.CreatedDate);
			References(x => x.Subscription)
				.Fetch.Join()
				.Cascade.None()
				.Column("SubscriptionId")
				.Not.Nullable();
		}
	}
}