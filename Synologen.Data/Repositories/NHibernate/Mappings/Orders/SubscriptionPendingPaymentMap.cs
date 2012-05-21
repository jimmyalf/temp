using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class SubscriptionPendingPaymentMap : ClassMap<SubscriptionPendingPayment>
	{
		public SubscriptionPendingPaymentMap()
		{
			Table("SynologenOrderSubscriptionPendingPayment");
			Id(x => x.Id);
			//Map(x => x.Amount).Not.Nullable();
			Map(x => x.TaxedAmount).Not.Nullable();
			Map(x => x.TaxFreeAmount).Not.Nullable();
			Map(x => x.Created).Not.Nullable();
			Map(x => x.HasBeenPayed).Not.Nullable();
			HasManyToMany(x => x.SubscriptionItems)
				.Table("SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem")
				.ParentKeyColumn("SubscriptionPendingPaymentId")
				.ChildKeyColumn("SubscriptionItemId")
				.Cascade.All();
		}
	}
}