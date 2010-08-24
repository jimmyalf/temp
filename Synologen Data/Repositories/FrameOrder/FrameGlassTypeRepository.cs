using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.FrameOrder
{
	public class FrameGlassTypeRepository : NHibernateRepository<FrameGlassType>, IFrameGlassTypeRepository
	{
		public FrameGlassTypeRepository(ISession session) : base(session) {  }
	}
}