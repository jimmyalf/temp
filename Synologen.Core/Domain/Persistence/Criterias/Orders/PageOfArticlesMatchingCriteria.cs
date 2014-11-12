using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfArticlesMatchingCriteria : SortedPagedSearchCriteria<Article>
	{
		public PageOfArticlesMatchingCriteria(string searchTerm) : base(searchTerm) {}
	}
}