using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class TransactionMap : ClassMap<SubscriptionTransaction>
	{
		public TransactionMap()
		{
			Table("SynologenLensSubscriptionTransaction");
			Id(x => x.Id);
			Map(x => x.Amount);
			Map(x => x.Reason)
				.CustomType(typeof(TransactionReason)); 
			Map(x => x.Type)
				.CustomType(typeof(TransactionType)); 
			Map(x => x.CreatedDate);
			References(x => x.Subscription)
				.Cascade.None()
				.Column("SubscriptionId")
				.Not.Nullable();
			References(x => x.Settlement)
				.Cascade.None()
				.Column("SettlementId")
				.Nullable();
			References(x => x.Article)
				.Cascade.None()
				.Column("ArticleId")
				.Nullable();
		}

	}
}
