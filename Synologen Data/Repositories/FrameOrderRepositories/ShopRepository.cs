using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
	public class ShopRepository : NHibernateReadonlyRepository<Shop>, IShopRepository
	{
		public ShopRepository(ISession session) : base(session) {}
	}
}