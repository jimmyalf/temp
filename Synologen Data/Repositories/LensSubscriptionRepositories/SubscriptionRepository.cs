using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories
{
	public class LensSubscriptionRepository : NHibernateRepository<Subscription>, ILensSubscriptionRepository
	{
		public LensSubscriptionRepository(ISession session) : base(session) {}
	}
}