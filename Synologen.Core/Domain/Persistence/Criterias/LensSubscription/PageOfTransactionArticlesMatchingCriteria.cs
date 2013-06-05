using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class PageOfTransactionArticlesMatchingCriteria : PagedSortedCriteria<TransactionArticle> 
	{
		public PageOfTransactionArticlesMatchingCriteria() { }
		public PageOfTransactionArticlesMatchingCriteria(string searchTerm)  { SearchTerm = searchTerm; }
		public string SearchTerm { get; set; }
	}
}