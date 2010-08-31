using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;


namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class PageOfFrameOrdersMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfFrameOrdersMatchingCriteria, FrameOrder>
	{
		public PageOfFrameOrdersMatchingCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(PageOfFrameOrdersMatchingCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Frame)
				.CreateAlias(x => x.GlassType)
				.CreateAlias(x => x.OrderingShop)
				.FilterByAny(filter =>
				{
					filter.By(x => x.Frame.Name);
					filter.By(x => x.GlassType.Name);
					filter.By(x => x.OrderingShop.Name);
				}, source.Search)
				.Sort(source.OrderBy, source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}
}

/*
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
 */