using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Subscription : Entity
	{
		public Subscription()
		{
			CreatedDate = DateTime.Now;
			Transactions = new SubscriptionTransaction[] {};
			Errors = new SubscriptionError[] {};
			PaymentInfo = new SubscriptionPaymentInfo();
			Status = SubscriptionStatus.Created;
		}
		public virtual Customer Customer { get; set; }
		public virtual SubscriptionPaymentInfo PaymentInfo { get; set; }
		public virtual IEnumerable<SubscriptionTransaction> Transactions { get; set; }
		public virtual SubscriptionStatus Status { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? ActivatedDate { get; set; }
		public virtual IEnumerable<SubscriptionError> Errors { get; set; }
	}
}