using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Subscription : Entity
	{
		public Customer Customer { get; set; }
		public string CustomerAccountNumber { get; set; }
		public string CustomerBank { get; set; }
		public Shop Shop { get; set; }
		public string BGCSubscriptionId { get; set; }
		public IEnumerable<SubscriptionTransaction> Transactions { get; private set; }
		public SubscriptionStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ExpierationDate { get; set; }
		public bool Expired { get; private set; }
	}
}