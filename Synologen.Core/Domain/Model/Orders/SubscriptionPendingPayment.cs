using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionPendingPayment : Entity
	{
		public SubscriptionPendingPayment()
		{
			Created = SystemTime.Now;
		}
	
		public virtual decimal Amount { get; set; }
		public virtual IEnumerable<SubscriptionItem> SubscriptionItems { get; set; }
		public virtual bool HasBeenPayed { get; set; }
		public virtual DateTime Created { get; private set; }
	}
}