using NHibernate;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories
{
	public class FrameColorRepository : NHibernateRepository<FrameColor>, IFrameColorRepository
	{
		public FrameColorRepository(ISession session) : base(session) {}

		public override void Delete(FrameColor entity)
		{
			if(FrameColorHasConnectedFrames(entity))
			{
				throw new SynologenDeleteItemHasConnectionsException("FrameColor cannot be deleted because it has connected Frames");
			}
			base.Delete(entity);
			return;
		}

		private bool FrameColorHasConnectedFrames(FrameColor entity)
		{
			var framesWithGivenColor = Session.CreateCriteria<Frame>()
				.Add(Restrictions.Eq("Color.Id", entity.Id))
				.GetCount().UniqueResult<long>();
			return (framesWithGivenColor > 0);
		}
	}
}