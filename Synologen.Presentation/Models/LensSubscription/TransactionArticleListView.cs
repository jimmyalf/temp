using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class TransactionArticleListView
	{
		public TransactionArticleListView()
		{
			Articles = Enumerable.Empty<TransactionArticleListItem>();
		}
		public IEnumerable<TransactionArticleListItem> Articles { get; set; }
		public string SearchTerm { get; set; }
	}
}