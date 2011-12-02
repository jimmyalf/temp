using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class ArticleTypesBySupplier : IActionCriteria
	{
		public ArticleTypesBySupplier(int selectedSupplierId)
		{
			SelectedSupplierId = selectedSupplierId;
		}

		public int SelectedSupplierId { get; set; }
	}
}