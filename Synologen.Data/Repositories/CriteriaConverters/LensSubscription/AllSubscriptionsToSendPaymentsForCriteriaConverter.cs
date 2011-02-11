using System;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

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
							Restrictions.IsNull("PaymentInfo.PaymentSentDate"),
							Restrictions.Not(
								Restrictions.And(
									Restrictions.Eq(
										Projections.SqlFunction("month", NHibernateUtil.DateTime, Projections.Property("PaymentInfo.PaymentSentDate")),
										DateTime.Now.Month),
									Restrictions.Eq(
										Projections.SqlFunction("year", NHibernateUtil.DateTime, Projections.Property("PaymentInfo.PaymentSentDate")),
										DateTime.Now.Year)
							))));
		}
	}
}
