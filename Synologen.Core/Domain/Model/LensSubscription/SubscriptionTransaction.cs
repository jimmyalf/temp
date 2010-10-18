using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class SubscriptionTransaction : Entity
	{
		public Subscription Subscription{ get; set; }
		public decimal Amount { get; set; }
		public TransactionType Type { get; set; }
		public TransactionReason Reason { get; set; }
		public DateTime Created{ get; set;}
	}
}