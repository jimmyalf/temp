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
			
			Component(x => x.Value, value =>
			{
				value.Map(x => x.Product).Column("ProductPrice");
				value.Map(x => x.Fee).Column("FeePrice");
			});
			Component(Reveal.Member<SubscriptionItem,SubscriptionItemAmount>("CustomMonthlyAmount"), value =>
			{
				value.Map(x => x.Product).Column("CustomMonthlyProduct");
				value.Map(x => x.Fee).Column("CustomMonthlyFee");
			});
			Map(x => x.CreatedDate).Not.Nullable();
		}
	}
}