using FluentNHibernate;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class SubscriptionItemMap : ClassMap<SubscriptionItem>
	{
		public SubscriptionItemMap()
		{
			Table("SynologenOrderSubscriptionItem");
			Id(x => x.Id);
			Map(x => x.WithdrawalsLimit).Nullable();
			Map(x => x.PerformedWithdrawals).Not.Nullable();
			References(x => x.Subscription).Column("SubscriptionId");
			Map(x => x.Version);
			Component(x => x.Value, value =>
			{
			    value.Map(x => x.Taxed).Column("ProductPrice");
			    value.Map(x => x.TaxFree).Column("FeePrice");
			});
			Component(Reveal.Member<SubscriptionItem, SubscriptionAmount>("CustomMonthlyAmount"), value =>
			{
			    value.Map(x => x.Taxed).Column("CustomMonthlyProduct");
			    value.Map(x => x.TaxFree).Column("CustomMonthlyFee");
			});
			Map(x => x.CreatedDate).Not.Nullable();
		    Map(x => x.Title).Nullable();
            Map(Reveal.Member<SubscriptionItem, object>("Active")).Not.Nullable();
		}
	}
}