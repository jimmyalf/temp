using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfOrdersMatchingCriteria : SortedPagedSearchCriteria<Order>
	{
		public PageOfOrdersMatchingCriteria(string searchTerm) : base(searchTerm){ }
	}
}