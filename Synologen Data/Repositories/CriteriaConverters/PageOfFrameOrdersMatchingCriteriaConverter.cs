using NHibernate;
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
				.CreateCriteria<Core.Domain.Model.FrameOrder.FrameOrder>()
				.SetAlias<Core.Domain.Model.FrameOrder.FrameOrder>(x => x.Frame)
				.SetAlias<Core.Domain.Model.FrameOrder.FrameOrder>(x => x.GlassType)
				.SetAlias<Core.Domain.Model.FrameOrder.FrameOrder>(x => x.OrderingShop)
				.Sort(source.OrderBy, source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}
}