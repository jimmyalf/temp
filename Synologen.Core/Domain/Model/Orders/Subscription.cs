using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class Subscription : Entity
	{
		public virtual OrderCustomer Customer { get; set; }
		public virtual string BankAccountNumber { get; set; }
		public virtual string ClearingNumber { get; set; }
		public virtual int AutogiroPayerId { get; set; }
		public virtual IEnumerable<SubscriptionItem> SubscriptionItems { get; set; }
		public virtual IEnumerable<SubscriptionTransaction> Transactions { get; set; }
		public virtual IEnumerable<SubscriptionError> Errors { get; set; }
		public virtual SubscriptionConsentStatus ConsentStatus { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? ActivatedDate { get; set; }
		public virtual bool Active { get; set; } 
	}
}