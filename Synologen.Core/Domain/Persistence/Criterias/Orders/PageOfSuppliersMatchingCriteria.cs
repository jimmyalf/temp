using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfSuppliersMatchingCriteria : SortedPagedSearchCriteria<ArticleSupplier>
	{
		public PageOfSuppliersMatchingCriteria(string searchTerm) : base(searchTerm){ }
	}
}