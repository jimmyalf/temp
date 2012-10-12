using System.Collections.Generic;
using FluentNHibernate;
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
			Component(Reveal.Member<SubscriptionPendingPayment,SubscriptionAmount>("Amount"), mapping =>
			{
			    mapping.Map(x => x.Taxed).Column("TaxedAmount");
			    mapping.Map(x => x.TaxFree).Column("TaxFreeAmount");
			});
			Map(x => x.Created).Not.Nullable();
			Map(x => x.HasBeenPayed).Not.Nullable();
			HasMany(Reveal.Member<SubscriptionPendingPayment, IEnumerable<SubscriptionPendingPaymentAmount>>("SubscriptionItemAmounts"))
				.KeyColumns.Add("SubscriptionPendingPaymentId")
				.Cascade.All();
		}
	}
}