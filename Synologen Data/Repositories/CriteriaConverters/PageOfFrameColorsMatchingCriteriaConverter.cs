using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class PageOfFrameColorsMatchingCriteriaConverter : IActionCriteriaConverter<PageOfFrameColorsMatchingCriteria, ICriteria> {
		private readonly ISession _session;
		public PageOfFrameColorsMatchingCriteriaConverter(ISession session) { _session = session; }

		public ICriteria Convert(PageOfFrameColorsMatchingCriteria source)
		{
			return _session
				.CreateCriteria<FrameColor>()
				.Sort(source.OrderBy, source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}
}