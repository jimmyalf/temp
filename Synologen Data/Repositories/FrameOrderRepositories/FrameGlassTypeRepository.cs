using NHibernate;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
	public class FrameGlassTypeRepository : NHibernateRepository<FrameGlassType>, IFrameGlassTypeRepository
	{
		public FrameGlassTypeRepository(ISession session) : base(session) {  }

		public override void Delete(FrameGlassType entity)
		{
			if(FrameGlassTypeHasConnectedOrders(entity))
			{
				throw new SynologenDeleteItemHasConnectionsException("FrameGlassType cannot be deleted because it has connected Orders");
			}
			base.Delete(entity);
			return;
		}

		private bool FrameGlassTypeHasConnectedOrders(FrameGlassType entity)
		{
			var framesWithGivenColor = Session.CreateCriteria<FrameOrder>()
				.Add(Restrictions.Eq("GlassType.Id", entity.Id))
				.GetCount().UniqueResult<long>();
			return (framesWithGivenColor > 0);
		}
	}
}