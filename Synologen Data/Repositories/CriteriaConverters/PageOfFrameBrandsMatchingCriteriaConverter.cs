using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class PageOfFrameBrandsMatchingCriteriaConverter: IActionCriteriaConverter<PageOfFrameBrandsMatchingCriteria, ICriteria> {
		private readonly ISession _session;
		public PageOfFrameBrandsMatchingCriteriaConverter(ISession session) { _session = session; }

		public ICriteria Convert(PageOfFrameBrandsMatchingCriteria source)
		{
			return _session
				.CreateCriteria<FrameBrand>()
				.Sort(source.OrderBy, source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}
}