using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class SubscriptionListView : CommonListView<SubscriptionListItem, Subscription>
	{
		public SubscriptionListView() { }

		public SubscriptionListView(string search, IEnumerable<Subscription> subscriptions) 
			: base(subscriptions, search) { }

		public override SubscriptionListItem Convert(Subscription item)
		{
			return new SubscriptionListItem
			{
				AccountNumber = item.BankAccountNumber,
				Customer = item.Customer.ParseName(x => x.FirstName, x => x.LastName),
				Shop = item.Shop.Name,
				Status = item.ConsentStatus.GetEnumDisplayName(),
				SubscriptionId = item.Id.ToString(),
				PaymentNumber = item.AutogiroPayerId.HasValue 
					? item.AutogiroPayerId.Value.ToString() 
					: null
			};
		}
	}

	public class SubscriptionListItem
	{
		public string SubscriptionId { get; set; }
		public string Shop { get; set; }
		public string Customer { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public string PaymentNumber { get; set; }
	}
}