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
		protected virtual SubscriptionAmount Amount { get; set; }
		protected virtual decimal? OldAmount { get; set; }
		public virtual TransactionType Type { get; set; }
		public virtual TransactionReason Reason { get; set; }
		public virtual DateTime CreatedDate { get; private set; }
		public virtual int? SettlementId { get; set; }
		public virtual SubscriptionPendingPayment PendingPayment { get; set; }
		public virtual SubscriptionAmount GetAmount()
		{
			return OldAmount.HasValue ? new SubscriptionAmount(OldAmount.Value, 0) : Amount;
		}
		public virtual void SetAmount(SubscriptionAmount amount)
		{
			Amount = amount;
		}
	}
}