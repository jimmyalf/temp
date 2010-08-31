using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories
{
	public class FrameBrandRepository : NHibernateRepository<FrameBrand>, IFrameBrandRepository
	{
		public FrameBrandRepository(ISession session) : base(session) {}

		public override void Delete(FrameBrand entity)
		{
			if(FrameBrandHasConnectedFrames(entity))
			{
				throw new SynologenDeleteItemHasConnectionsException("FrameBrand cannot be deleted because it has connected Frames");
			}
			base.Delete(entity);
			return;
		}

		private bool FrameBrandHasConnectedFrames(FrameBrand entity)
		{
			var framesWithGivenColor = Session.CreateCriteria<Frame>()
				.Add(Restrictions.Eq("Brand.Id", entity.Id))
				.ToCountCriteria().UniqueResult<long>();
			return (framesWithGivenColor > 0);
		}
	}
}