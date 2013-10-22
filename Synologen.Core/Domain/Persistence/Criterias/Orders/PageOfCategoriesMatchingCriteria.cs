using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfCategoriesMatchingCriteria : SortedPagedSearchCriteria<ArticleCategory> 
	{
		public PageOfCategoriesMatchingCriteria(string searchTerm) : base(searchTerm){ }
	}
}