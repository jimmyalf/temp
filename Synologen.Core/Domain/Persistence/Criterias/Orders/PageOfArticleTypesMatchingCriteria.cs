using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfArticleTypesMatchingCriteria : SortedPagedSearchCriteria<ArticleType>
	{
		public PageOfArticleTypesMatchingCriteria(string searchTerm) : base(searchTerm){ }
	}
}