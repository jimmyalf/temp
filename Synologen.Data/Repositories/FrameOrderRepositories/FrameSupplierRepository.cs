using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
    public class FrameSupplierRepository : NHibernateRepository<FrameSupplier>, IFrameSupplierRepository
    {
        public FrameSupplierRepository(ISession session) : base(session) {}

    }
}