using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionTransaction : Entity
	{
		public SubscriptionTransaction()
		{
			CreatedDate = SystemTime.Now;
		}
		public virtual Subscription Subscription { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual TransactionType Type { get; set; }
		public virtual TransactionReason Reason { get; set; }
		public virtual DateTime CreatedDate { get; private set; }
		public virtual int? SettlementId { get; set; }
		//public virtual Article Article { get; set;}
	}
}