using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class PageOfTransactionArticlesMatchingCriteriaConverter : NHibernateActionCriteriaConverter<PageOfTransactionArticlesMatchingCriteria,TransactionArticle>
	{
		public PageOfTransactionArticlesMatchingCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(PageOfTransactionArticlesMatchingCriteria source) 
		{
			return Criteria
				.FilterBy(x => x.Name, source.SearchTerm)
				.Sort(source.OrderBy,source.SortAscending)
				.Page(source.Page, source.PageSize);
		}
	}
}