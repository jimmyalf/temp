using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class AllArticlesWithArticleTypeCriteria : IActionCriteria
	{
		public AllArticlesWithArticleTypeCriteria(int id)
		{
			ArticleTypeId = id;
		}

		public int ArticleTypeId { get; set; }
	}
}