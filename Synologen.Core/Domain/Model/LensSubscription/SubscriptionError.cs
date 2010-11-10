using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class SubscriptionError : Entity
	{
		public virtual Subscription Subscription { get; set; }
		public virtual SubscriptionErrorType Type { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? HandledDate { get; set; }
		public virtual bool IsHandled { get; set; }
	}
}
