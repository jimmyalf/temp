using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class AllOrdersWithArticleCriteria : IActionCriteria
	{
		public int ArticleId { get; set; }

		public AllOrdersWithArticleCriteria(int articleId)
		{
			ArticleId = articleId;
		}
	}
}