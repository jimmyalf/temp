using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;


namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
	public class PageOfFramesMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfFramesMatchingCriteria, Frame> {
		public PageOfFramesMatchingCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(PageOfFramesMatchingCriteria source)
		{
			
			return Criteria
				.CreateAlias(x => x.Color)
				.CreateAlias(x => x.Brand)
				.FilterByAny(filter =>
				{
					filter.By(x => x.Name);
					filter.By(x => x.ArticleNumber);
					filter.By(x => x.Color.Name);
					filter.By(x => x.Brand.Name);
				}, source.NameLike)
				.Sort(source.OrderBy, source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}

	
}