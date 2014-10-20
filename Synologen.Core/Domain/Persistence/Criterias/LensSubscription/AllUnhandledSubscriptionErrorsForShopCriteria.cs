using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class AllUnhandledSubscriptionErrorsForShopCriteria : IActionCriteria 
	{
		public AllUnhandledSubscriptionErrorsForShopCriteria(int shopId)
		{
			ShopId = shopId;
		}
		public int ShopId { get; set; }
	}
}