using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllSubscriptionsToSendPaymentsForCriteriaConverter : NHibernateActionCriteriaConverter<AllSubscriptionsToSendPaymentsForCriteria, Subscription>, IActionCriteria
	{
		public AllSubscriptionsToSendPaymentsForCriteriaConverter(ISession session) : base(session) {}

        public override ICriteria Convert(AllSubscriptionsToSendPaymentsForCriteria source)
		{
			return Criteria
				.FilterEqual(x => x.ConsentStatus, SubscriptionConsentStatus.Accepted)
				.FilterEqual(x => x.Active, true)
				.Add(Restrictions.Or(
					Restrictions.IsNull(Property(x => x.LastPaymentSent)),
					Restrictions.Lt(Property(x => x.LastPaymentSent), FirstDateInCurrentMonth)
				))
				.SetFetchMode(Property(x => x.SubscriptionItems), FetchMode.Join)
				.SetResultTransformer(new DistinctRootEntityResultTransformer());
		}

		private static DateTime FirstDateInCurrentMonth
		{
			get { return new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, 1); }
		}
	}
}
