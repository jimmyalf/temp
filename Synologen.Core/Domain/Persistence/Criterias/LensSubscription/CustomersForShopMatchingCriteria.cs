using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class CustomersForShopMatchingCriteria : IActionCriteria
	{
		public int ShopId { get; set; }
		public string SearchTerm { get; set; }
		public string OrderBy { get; set; }
		public bool SortAscending { get; set; }
	}
}
