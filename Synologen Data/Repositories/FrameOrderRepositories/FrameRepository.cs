using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
	public class FrameRepository : NHibernateRepository<Frame>, IFrameRepository
	{
		public FrameRepository(ISession session) : base(session) {}

		public override void Delete(Frame entity)
		{
			if(FrameHasConnectedOrders(entity))
			{
				throw new SynologenDeleteItemHasConnectionsException("Frame cannot be deleted because it has connected Orders");
			}
			base.Delete(entity);
			return;
		}

		private bool FrameHasConnectedOrders(Frame entity)
		{
			var ordersWithGivenFrame = Session.CreateCriteria<Core.Domain.Model.FrameOrder.FrameOrder>()
				.Add(Restrictions.Eq("Frame.Id", entity.Id))
				.ToCountCriteria().UniqueResult<long>();
			return (ordersWithGivenFrame > 0);
		}
	}
}