using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class ShopSubscriptionErrorListModel
	{
		public ShopSubscriptionErrorListModel()
		{
			UnhandledErrors = Enumerable.Empty<ShopSubscriptionErrorListItemModel>();
		}
		public IEnumerable<ShopSubscriptionErrorListItemModel> UnhandledErrors { get; set; }
	}
}