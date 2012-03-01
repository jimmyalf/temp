using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionItem : Entity
	{
		public SubscriptionItem()
		{
			CreatedDate = SystemTime.Now;
		}

		public virtual DateTime CreatedDate { get; private set; }
		public virtual Subscription Subscription { get; set; }
		public virtual int WithdrawalsLimit { get; set; }
		public virtual int PerformedWithdrawals { get; set; }
		public virtual decimal TaxedAmount { get; set; }
		public virtual decimal TaxFreeAmount { get; set; }
		public virtual decimal AmountForAutogiroWithdrawal { get { return TaxedAmount + TaxFreeAmount; } }
		public virtual bool IsActive
		{ 
			get
			{
				//if(!WithdrawalsLimit.HasValue) return true; //Continuous withdrawal subscription item
				return PerformedWithdrawals < WithdrawalsLimit;//.Value; // Subscription item has limit
			} 
		}
	}
}