using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories
{
	public class SubscriptionItemRepository : NHibernateRepository<SubscriptionItem>, ISubscriptionItemRepository
	{
		public SubscriptionItemRepository(ISession session) : base(session) {}
	}
}