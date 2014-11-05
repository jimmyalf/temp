using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public abstract class SortedPagedSearchCriteria<TType> : PagedSortedCriteria<TType> where TType : class
	{
		protected SortedPagedSearchCriteria(string searchTerm) { SearchTerm = searchTerm; }
		public string SearchTerm { get; set; }
	}
}