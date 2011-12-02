using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class SuppliersByCategory : IActionCriteria
	{
		public SuppliersByCategory(int selectedCategoryId)
		{
			SelectedCategoryId = selectedCategoryId;
		}

		public int SelectedCategoryId { get; set; }
	}
}