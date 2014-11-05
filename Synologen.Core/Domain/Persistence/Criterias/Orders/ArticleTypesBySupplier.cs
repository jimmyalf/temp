using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class ArticleTypesBySupplier : IActionCriteria
	{
		public ArticleTypesBySupplier(int selectedCategoryId)
		{
			SelectedCategoryId = selectedCategoryId;
		}

		public int SelectedCategoryId { get; set; }
	}
}