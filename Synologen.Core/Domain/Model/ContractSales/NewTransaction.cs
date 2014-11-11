using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class NewTransaction : Entity
	{
		//public virtual decimal Amount { get; set; }
		public virtual NewSubscription Subscription { get; set; }
		public virtual PendingPayment PendingPayment { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		protected virtual SubscriptionAmount Amount { get; set; }
		protected virtual decimal? OldAmount { get; set; }
		public virtual SubscriptionAmount GetAmount()
		{
			/* Older transactions will have pending payments with amounts, new ones will not*/
			if(PendingPayment != null)
			{
				if(PendingPayment.TaxFreeAmount != default(decimal) && PendingPayment.TaxedAmount != default(decimal))
				{
					return new SubscriptionAmount(PendingPayment.TaxedAmount, PendingPayment.TaxFreeAmount);
				}
			}
			/* New transactions have detailed amounts */
			return OldAmount.HasValue ? new SubscriptionAmount(OldAmount.Value, 0) : Amount;
		}
	}
}