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
			Active = false;
			ConsentStatus = SubscriptionConsentStatus.NotSent;
		}

		public virtual SubscriptionConsentStatus ConsentStatus { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual SubscriptionPaymentInfo PaymentInfo { get; set; }
		public virtual IEnumerable<SubscriptionTransaction> Transactions { get; set; }
		public virtual bool Active { get; set; } 
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? ActivatedDate { get; set; }
		public virtual IEnumerable<SubscriptionError> Errors { get; set; }
		public virtual string Notes { get; set; }
		public virtual int? BankGiroPayerNumber { get; set; }
	}
}