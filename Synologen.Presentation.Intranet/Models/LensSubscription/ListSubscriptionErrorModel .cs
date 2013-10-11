using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class ListSubscriptionErrorModel
	{
		public IEnumerable<SubscriptionErrorListItemModel> List { get; set; }
		public bool HasErrors { get; set; }
	}
}
