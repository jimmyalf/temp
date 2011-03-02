using System;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories
{
	public class SubscriptionRepository : NHibernateRepository<Subscription>, ISubscriptionRepository
	{
		public SubscriptionRepository(ISession session) : base(session) {}
		public Subscription GetByBankgiroPayerId(int bankgiroPayerId) { throw new NotImplementedException(); }
	}
}