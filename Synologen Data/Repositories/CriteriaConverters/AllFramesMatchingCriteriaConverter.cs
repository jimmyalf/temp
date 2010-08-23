using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class AllFramesMatchingCriteriaConverter : IActionCriteriaConverter<AllFramesMatchingCriteria, ICriteria> {
		private readonly ISession _session;
		public AllFramesMatchingCriteriaConverter(ISession session) { _session = session; }

		public ICriteria Convert(AllFramesMatchingCriteria source)
		{
			return _session
				.CreateCriteria<Frame>()
				.FilterName<Frame>(x => x.Name, source.NameLike);
		}
	}
}