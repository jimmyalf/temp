using System.Collections.Generic;
using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class SubscriptionListView
	{
		[DisplayName("Filtrera")]
		public string SearchTerm { get; set; }

		public IEnumerable<SubscriptionListItemView> List { get; set; }
	}
}