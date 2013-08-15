using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class AllSubscriptionsToSendConsentsForCriteriaConverter : NHibernateActionCriteriaConverter<AllSubscriptionsToSendConsentsForCriteria, Subscription>, IActionCriteria
	{
		public AllSubscriptionsToSendConsentsForCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllSubscriptionsToSendConsentsForCriteria source)
		{
			return Criteria
				.FilterEqual(x => x.Active, true)
				.FilterEqual(x => x.ConsentStatus, SubscriptionConsentStatus.NotSent);
		}
	}
}