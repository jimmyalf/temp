using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfCategoriesMatchingCriteria : PagedSortedCriteria<ArticleCategory> 
	{
		public PageOfCategoriesMatchingCriteria(string searchTerm) { SearchTerm = searchTerm; }
		public string SearchTerm { get; set; }
	}
}