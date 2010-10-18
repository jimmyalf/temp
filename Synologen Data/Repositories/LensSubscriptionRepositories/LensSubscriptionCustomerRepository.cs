using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories
{
	public class LensSubscriptionCustomerRepository : NHibernateRepository<Customer>, ILensSubscriptionCustomerRepository
	{
		public LensSubscriptionCustomerRepository(ISession session) : base(session) {}
	}
}