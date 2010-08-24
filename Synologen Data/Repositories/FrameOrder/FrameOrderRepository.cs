using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrder
{
	public class FrameOrderRepository : NHibernateRepository<Core.Domain.Model.FrameOrder.FrameOrder>, IFrameOrderRepository
	{
		public FrameOrderRepository(ISession session) : base(session) {}
	}
}