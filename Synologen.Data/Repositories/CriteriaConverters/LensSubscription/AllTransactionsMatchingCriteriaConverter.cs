using System;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class AllTransactionsMatchingCriteriaConverter : NHibernateActionCriteriaConverter<AllTransactionsMatchingCriteria, SubscriptionTransaction>
	{
		public AllTransactionsMatchingCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(AllTransactionsMatchingCriteria source) 
		{
			var criteria = Criteria;
			switch (source.SettlementStatus)
			{
				case SettlementStatus.Any: break;
				case SettlementStatus.HasSettlement:
					criteria.Add(Restrictions.IsNotNull(Property(x => x.Settlement)));
					break;
				case SettlementStatus.DoesNotHaveSettlement:
					criteria.Add(Restrictions.IsNull(Property(x => x.Settlement)));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			criteria = (!source.Type.HasValue) ? criteria : criteria.FilterEqual(x => x.Type, source.Type);
			criteria = (!source.Reason.HasValue) ? criteria : criteria.FilterEqual(x => x.Reason, source.Reason);
			return criteria;
		}
	}
}