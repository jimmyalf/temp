using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class PageOfCategoriesMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfCategoriesMatchingCriteria,ArticleCategory>
	{
		public PageOfCategoriesMatchingCriteriaConverter(ISession session) : base(session) {}

		public override ICriteria Convert(PageOfCategoriesMatchingCriteria source)
		{
			return Criteria
				.SynologenFilterByAny(filter =>
				{ 
					filter.Like(x => x.Name);
					filter.IfInt(source.SearchTerm, parsedValue => filter.Equal(x => x.Id, parsedValue));
				}, source.SearchTerm)
				.Page(source.Page, source.PageSize)
				.Sort(source.OrderBy, source.SortAscending);
		}
	}
}