using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class AllSubscriptionsForShopCriteria : IActionCriteria
	{
		public int ShopId { get; set; }
		public bool SortAscending { get; set; }

		public AllSubscriptionsForShopCriteria(int shopId, bool sortAscending = false)
		{
			ShopId = shopId;
			SortAscending = sortAscending;
		}
	}
}