using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class PageOfOrdersMatchingCriteria : PagedSortedCriteria<Order> 
	{
		public PageOfOrdersMatchingCriteria(string searchTerm) { SearchTerm = searchTerm; }
		public string SearchTerm { get; set; }
	}
}