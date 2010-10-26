using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class SubscriptionListItemView
	{
		[DisplayName("Id")]
		public int SubscriptionId { get; set; }

		[DisplayName("Kund")]
		public string CustomerName { get; set; }

		[DisplayName("Butik")]
		public string ShopName { get; set; }

		[DisplayName("Status")]
		public string Status { get; set; }
	}
}