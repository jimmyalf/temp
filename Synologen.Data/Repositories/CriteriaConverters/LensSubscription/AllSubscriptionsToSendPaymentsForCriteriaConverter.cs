using System;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
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
					Restrictions.IsNull(Property(x => x.PaymentInfo.PaymentSentDate)),
					Restrictions.Lt(Property(x => x.PaymentInfo.PaymentSentDate), FirstDateInCurrentMonth)
				));
		}

		private static DateTime FirstDateInCurrentMonth
		{
			get { return new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, 1); }
		}
	}
}
