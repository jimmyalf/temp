using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
	public class ShopSubscriptionsModel
	{
		public ShopSubscriptionsModel()
		{
			List = new SubscriptionListItem[]{};
		}
		public ShopSubscriptionsModel(IEnumerable<SubscriptionListItem> items)
		{
			List = items;
		}
		public IEnumerable<SubscriptionListItem> List { get; set; }
	}

	public class SubscriptionListItem
	{
		public string CustomerName { get; set; }
		public string MonthlyAmount { get; set; }
		public string CurrentBalance { get; set; }
		public string Status { get; set; }
		public string SubscriptionDetailsUrl { get; set; }
		public string CustomerDetailsUrl { get; set; }
		public string BankAccountNumber { get; set; }
	}
}