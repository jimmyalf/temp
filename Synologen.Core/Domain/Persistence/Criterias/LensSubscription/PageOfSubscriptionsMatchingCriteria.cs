using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class PageOfSubscriptionsMatchingCriteria : PagedSortedCriteria<Subscription> 
	{
		public PageOfSubscriptionsMatchingCriteria() { }
		public PageOfSubscriptionsMatchingCriteria(string searchTerm) { SearchTerm = searchTerm; }

		public string SearchTerm { get; set; }
	}
}