using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class ActiveAndConsentedSubscriptionsForCustomerCritieriaConverter : NHibernateActionCriteriaConverter<ActiveAndConsentedSubscriptionsForCustomerCritieria,Subscription>
	{
		public ActiveAndConsentedSubscriptionsForCustomerCritieriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(ActiveAndConsentedSubscriptionsForCustomerCritieria source)
		{
			return Criteria
				.FilterEqual(x => x.Active, true)
				.FilterEqual(x => x.Customer.Id, source.CustomerId)
				.Add(Restrictions.Eq(Property(x => x.ConsentStatus), SubscriptionConsentStatus.Accepted.ToInteger()));
		}
	}
}