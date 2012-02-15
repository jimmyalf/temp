using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories
{
	public class SubscriptionRepository : NHibernateRepository<Subscription>, ISubscriptionRepository
	{
		public SubscriptionRepository(ISession session) : base(session) {}
		public Subscription GetByBankgiroPayerId(int autogiroPayerId)
		{
			return Session.CreateCriteriaOf<Subscription>()
				.FilterEqual(x => x.AutogiroPayerId, autogiroPayerId)
				.UniqueResult<Subscription>();
		}
	}
}