using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class AllSubscriptionsForShopCriteria : IActionCriteria
	{
		public int ShopId { get; set; }
		public bool SortAscending { get; set; }

		public AllSubscriptionsForShopCriteria(int shopId)
		{
			ShopId = shopId;
		}
		public AllSubscriptionsForShopCriteria(int shopId, bool sortAscending)
		{
			ShopId = shopId;
			SortAscending = sortAscending;
		}
	}
}