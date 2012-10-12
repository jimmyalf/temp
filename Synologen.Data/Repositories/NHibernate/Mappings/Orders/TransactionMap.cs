using FluentNHibernate;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders
{
	public class TransactionMap : ClassMap<SubscriptionTransaction>
	{
		public TransactionMap()
		{
			Table("SynologenOrderTransaction");
			Id(x => x.Id);
			Component(Reveal.Member<SubscriptionTransaction,SubscriptionAmount>("Amount"), mapping =>
			{ 
			    mapping.Map(x => x.TaxFree).Column("TaxFreeAmount");
			    mapping.Map(x => x.Taxed).Column("TaxedAmount");
			});
			Map(Reveal.Member<SubscriptionTransaction>("OldAmount")).Column("Amount");
			Map(x => x.Reason).CustomType<TransactionReason>(); 
			Map(x => x.Type).CustomType<TransactionType>(); 
			Map(x => x.CreatedDate);
			References(x => x.Subscription).Column("SubscriptionId").Not.Nullable();
			References(x => x.PendingPayment).Column("PendingPaymentId").Nullable();
			Map(x => x.SettlementId).Nullable();
		}

	}
}
