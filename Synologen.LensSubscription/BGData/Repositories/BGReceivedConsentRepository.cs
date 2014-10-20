using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;

namespace Synologen.LensSubscription.BGData.Repositories
{
    public class BGReceivedConsentRepository : NHibernateRepository<BGReceivedConsent>, IBGReceivedConsentRepository
    {
        public BGReceivedConsentRepository(ISession session) : base(session) {}
    }
}
