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
		public virtual decimal ProductPrice { get; set; }
		public virtual decimal FeePrice { get; set; }
		public virtual decimal TotalValue { get { return ProductPrice + FeePrice; } }

		public virtual decimal MonthlyWithdrawalAmount
		{
			get
			{
				return WithdrawalsLimit <= 0 ? 0 : Math.Round(TotalValue / WithdrawalsLimit, 2);
			}
		}
		public virtual bool IsActive
		{ 
			get { return PerformedWithdrawals < WithdrawalsLimit; } 
		}
	}
}