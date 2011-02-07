using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class TransactionsForSubscriptionMatchingCriteriaConverter : NHibernateActionCriteriaConverter<TransactionsForSubscriptionMatchingCriteria, SubscriptionTransaction>
	{
		public TransactionsForSubscriptionMatchingCriteriaConverter(ISession session) : base(session)
		{
		}

		public override ICriteria Convert(TransactionsForSubscriptionMatchingCriteria source)
		{
			return Criteria.FilterEqual(x => x.Subscription.Id, source.SubscriptionId)
				.SetFetchMode("SettlementId", FetchMode.Join)
				.Sort("CreatedDate", false);
		}
	}
}
