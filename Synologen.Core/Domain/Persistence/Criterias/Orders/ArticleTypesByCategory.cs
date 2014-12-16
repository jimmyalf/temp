using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class ArticleTypesByCategory : IActionCriteria
	{
		public ArticleTypesByCategory(int selectedCategoryId)
		{
			SelectedCategoryId = selectedCategoryId;
		}

		public int SelectedCategoryId { get; set; }
	}
}