using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories
{
	public class SubscriptionErrorRepository : NHibernateRepository<SubscriptionError>, ISubscriptionErrorRepository
	{
		public SubscriptionErrorRepository(ISession session) : base(session) { }
	}
}
