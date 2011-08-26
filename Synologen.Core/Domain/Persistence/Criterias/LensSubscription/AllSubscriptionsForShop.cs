using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class AllSubscriptionsForShopCriteria : IActionCriteria
	{
		public int ShopId { get; set; }

		public AllSubscriptionsForShopCriteria(int shopId)
		{
			ShopId = shopId;
		}
	}
}