using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class SubscriptionMap : ClassMap<Subscription>
	{
		public SubscriptionMap()
		{
			Table("SynologenLensSubscription");
			Id(x => x.Id);
			References(x => x.Customer)
				.Fetch.Join()
				.Cascade.None()
				.Column("CustomerId")
				.Not.Nullable();
		}
	}
}