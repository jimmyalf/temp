using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories
{
	public class FrameGlassTypeRepository : NHibernateRepository<FrameGlassType>, IFrameGlassTypeRepository
	{
		public FrameGlassTypeRepository(ISession session) : base(session) {  }
	}
}