using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
	public class ShopRepository : NHibernateReadonlyRepository<Shop>, IShopRepository
	{
		public ShopRepository(ISession session) : base(session) {}
	}
}