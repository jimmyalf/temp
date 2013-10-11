using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class ActiveSubscriptionsForCustomerCritieriaConverter : NHibernateActionCriteriaConverter<ActiveSubscriptionsForCustomerCritieria,Subscription>
	{
		public ActiveSubscriptionsForCustomerCritieriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(ActiveSubscriptionsForCustomerCritieria source)
		{
			return Criteria
				.FilterEqual(x => x.Active, true)
				.FilterEqual(x => x.Customer.Id, source.CustomerId);
		}
	}
}