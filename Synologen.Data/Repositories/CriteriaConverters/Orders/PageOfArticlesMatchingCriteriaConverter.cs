using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class PageOfArticlesMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfArticlesMatchingCriteria,Article>
	{
		public PageOfArticlesMatchingCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(PageOfArticlesMatchingCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.ArticleSupplier)
				.CreateAlias(x => x.ArticleType)
				.SynologenFilterByAny(filter =>
				{ 
					filter.Like(x => x.Name);
					filter.IfInt(source.SearchTerm, parsedValue => filter.Equal(x => x.Id, parsedValue));
					filter.Like(x => x.ArticleSupplier.Name);
					filter.Like(x => x.ArticleType.Name);
				}, source.SearchTerm)
				.Page(source.Page, source.PageSize)
				.Sort(source.OrderBy, source.SortAscending);
		}
	}
}