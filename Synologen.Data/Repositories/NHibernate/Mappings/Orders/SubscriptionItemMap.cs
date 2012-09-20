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
			Map(x => x.ProductPrice).Not.Nullable();
			Map(x => x.FeePrice).Not.Nullable();
			Map(x => x.MonthlyFee).Nullable();
			Map(x => x.MonthlyPrice).Nullable();
			Map(x => x.CreatedDate).Not.Nullable();
		}
	}
}