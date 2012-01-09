using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails;

namespace Spinit.Wpc.Synologen.Data.Repositories.ShopDetailsRepositories
{
    public class ShopRepository : NHibernateReadonlyRepository<Shop>, IShopRepository
    {
        public ShopRepository(ISession session) : base(session) { }
    }
}
