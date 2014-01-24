using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
	public class FrameOrderRepository : NHibernateRepository<Core.Domain.Model.FrameOrder.FrameOrder>, IFrameOrderRepository
	{
		public FrameOrderRepository(ISession session) : base(session) {}
	}
}