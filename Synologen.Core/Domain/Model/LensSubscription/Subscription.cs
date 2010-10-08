using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Subscription : Entity
	{
		public Customer Customer { get; set; }
		public Shop Shop { get; set; }
		public SubscriptionPaymentInfo PaymentInfo { get; set; }
		public IEnumerable<SubscriptionTransaction> Transactions { get; private set; }
		public SubscriptionStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ExpierationDate { get; set; }
		public DateTime ActivatedDate { get; set; }
		public bool Expired { get; private set; }
	}
}