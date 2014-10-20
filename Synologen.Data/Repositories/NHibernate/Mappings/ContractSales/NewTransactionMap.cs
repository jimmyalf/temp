using FluentNHibernate;
using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.ContractSales
{
	public class NewTransactionMap : ClassMap<NewTransaction>
	{
		public NewTransactionMap()
		{
			Table("SynologenOrderTransaction");
			Id(x => x.Id);
			Component(Reveal.Member<NewTransaction,SubscriptionAmount>("Amount"), mapping =>
			{ 
			    mapping.Map(x => x.TaxFree).Column("TaxFreeAmount");
			    mapping.Map(x => x.Taxed).Column("TaxedAmount");
			});
			Map(Reveal.Member<NewTransaction>("OldAmount")).Column("Amount");
			Map(x => x.CreatedDate);
			References(x => x.Subscription)
				.Fetch.Join()
				.Cascade.None()
				.Column("SubscriptionId")
				.Not.Nullable();
			References(x => x.PendingPayment)
				.Fetch.Join()
				.Cascade.None()
				.Column("PendingPaymentId")
				.Nullable();
		}
	}
}