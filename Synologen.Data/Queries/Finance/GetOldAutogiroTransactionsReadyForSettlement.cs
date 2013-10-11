using System.Collections.Generic;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Queries.Finance
{
	public class GetOldAutogiroTransactionsReadyForSettlement : Query<IEnumerable<SubscriptionTransaction>>
	{
		public override IEnumerable<SubscriptionTransaction> Execute()
		{
			return Session.CreateCriteriaOf<SubscriptionTransaction>()
				.FilterEqual(x => x.Reason, TransactionReason.Payment)
				.FilterEqual(x => x.Type, TransactionType.Deposit)
				.Add(Restrictions.IsNull(Property<SubscriptionTransaction>(x => x.Settlement)))
				.List<SubscriptionTransaction>();
		}
	}
}