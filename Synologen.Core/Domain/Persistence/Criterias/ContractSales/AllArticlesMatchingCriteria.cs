using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales
{
	public class AllArticlesMatchingCriteria : PagedSortedCriteria<Article>
	{
		public string SearchTerm { get; set; }

		public AllArticlesMatchingCriteria(string searchTerm)
		{
			SearchTerm = searchTerm;
		}
	}
}