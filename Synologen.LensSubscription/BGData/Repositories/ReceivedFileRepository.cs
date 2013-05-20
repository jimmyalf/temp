using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;

namespace Synologen.LensSubscription.BGData.Repositories
{
    public class ReceivedFileRepository : NHibernateRepository<ReceivedFileSection>, IReceivedFileRepository
    {
        public ReceivedFileRepository(ISession session) : base(session) { }
    }
}
