using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;

namespace Synologen.LensSubscription.BGData.Repositories
{
    public class BGRecievedErrorRepository : NHibernateRepository<BGReceivedError>, IBGReceivedErrorRepository
    {
        public BGRecievedErrorRepository(ISession session) : base(session)
        {
        }
    }
}
