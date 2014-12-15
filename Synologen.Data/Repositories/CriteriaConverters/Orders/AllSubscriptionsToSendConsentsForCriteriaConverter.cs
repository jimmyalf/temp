using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
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