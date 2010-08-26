using NHibernate;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class PageOfFrameOrdersMatchingCriteriaConverter : IActionCriteriaConverter<PageOfFrameOrdersMatchingCriteria, ICriteria>
	{
		private readonly ISession _session;
		public PageOfFrameOrdersMatchingCriteriaConverter(ISession session) { _session = session; }
		public ICriteria Convert(PageOfFrameOrdersMatchingCriteria source)
		{
			return _session
				.CreateCriteria<FrameOrder>()
				.SetAlias<FrameOrder>(x => x.Frame)
				.SetAlias<FrameOrder>(x => x.GlassType)
				.SetAlias<FrameOrder>(x => x.OrderingShop)
				.Add(Restrictions.Disjunction()
					.ApplyFilterInJunction<FrameOrder>(x => x.Frame.Name, source.Search)
					.ApplyFilterInJunction<FrameOrder>(x => x.GlassType.Name, source.Search)
					.ApplyFilterInJunction<FrameOrder>(x => x.OrderingShop.Name, source.Search)
				)
				.Sort(source.OrderBy, source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}
}