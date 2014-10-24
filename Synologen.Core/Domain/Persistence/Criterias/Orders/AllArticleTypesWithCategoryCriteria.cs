using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class AllArticleTypesWithCategoryCriteria : IActionCriteria
	{
		public AllArticleTypesWithCategoryCriteria(int id)
		{
			ArticleCategoryId = id;
		}
		public int ArticleCategoryId { get; set; }
	}
}