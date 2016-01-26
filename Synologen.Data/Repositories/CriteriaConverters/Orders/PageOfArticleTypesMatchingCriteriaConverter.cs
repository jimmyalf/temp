using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class PageOfArticleTypesMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfArticleTypesMatchingCriteria,ArticleType>
	{
		public PageOfArticleTypesMatchingCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(PageOfArticleTypesMatchingCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Category)
				.SynologenFilterByAny(filter =>
				{ 
					filter.Like(x => x.Name);
					filter.IfInt(source.SearchTerm, parsedValue => filter.Equal(x => x.Id, parsedValue));
					filter.Like(x => x.Category.Name);
				}, source.SearchTerm)
				.Page(source.Page, source.PageSize)
				.Sort(source.OrderBy, source.SortAscending);
		}
	}
}