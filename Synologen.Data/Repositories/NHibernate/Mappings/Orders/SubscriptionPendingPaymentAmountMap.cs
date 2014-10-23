using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class SubscriptionPendingPaymentAmountMap : ClassMap<SubscriptionPendingPaymentAmount>
	{
		public SubscriptionPendingPaymentAmountMap()
		{
			Table("SynologenOrderSubscriptionPendingPaymentAmount");
			CompositeId()
				.KeyReference(x => x.PendingPayment,"SubscriptionPendingPaymentId")
				.KeyReference(x => x.SubscriptionItem, "SubscriptionItemId");
			Component(x => x.Amount, mapping =>

			{
				mapping.Map(x => x.Taxed).Column("TaxedAmount");
				mapping.Map(x => x.TaxFree).Column("TaxFreeAmount");
			});
		}
	}
}