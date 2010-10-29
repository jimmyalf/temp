using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class SubscriptionTransaction : Entity
	{
		public virtual Subscription Subscription { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual TransactionType Type { get; set; }
		public virtual TransactionReason Reason { get; set; }
		public virtual DateTime CreatedDate { get; set; }
	}
}