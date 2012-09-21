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
		public virtual int? WithdrawalsLimit { get; protected set; }
		public virtual int PerformedWithdrawals { get; set; }
		public virtual SubscriptionItemAmount Value { get; protected set; }
		protected virtual SubscriptionItemAmount CustomMonthlyAmount { get; set; }

		public virtual SubscriptionItem Setup(int withdrawalLimit, decimal totalPrice, decimal totalFee)
		{
			WithdrawalsLimit = withdrawalLimit;
			Value = new SubscriptionItemAmount(totalPrice, totalFee);
			CustomMonthlyAmount = null;
			return this;
		}

		public virtual SubscriptionItem Setup(decimal monthlyPrice, decimal monthlyFee, decimal totalPrice, decimal totalFee)
		{
			WithdrawalsLimit = null;
			Value = new SubscriptionItemAmount(totalPrice, totalFee);
			CustomMonthlyAmount = new SubscriptionItemAmount(monthlyPrice, monthlyFee);
			return this;
		}

		public virtual bool IsOngoing { get { return !WithdrawalsLimit.HasValue; } }

		public virtual SubscriptionItemAmount MonthlyWithdrawal
		{
			get
			{
				var fee = IsOngoing ? CustomMonthlyAmount.Fee : WithdrawalsLimit <= 0 ? 0 : Math.Round(Value.Fee / WithdrawalsLimit.Value, 2);
				var product = IsOngoing ? CustomMonthlyAmount.Product : WithdrawalsLimit <= 0 ? 0 : Math.Round(Value.Product / WithdrawalsLimit.Value, 2);
				return new SubscriptionItemAmount(product, fee);
			}
		}

		public virtual bool IsActive
		{ 
			get { return IsOngoing || PerformedWithdrawals < WithdrawalsLimit; } 
		}
	}

	public class SubscriptionItemAmount 
	{
		public SubscriptionItemAmount() { }
		public SubscriptionItemAmount(decimal product, decimal fee)
		{
			Product = product;
			Fee = fee;
		}
		public virtual decimal Product { get; protected set; }
		public virtual decimal Fee { get; protected set; }
		public virtual decimal Total { get { return Product + Fee; } }
	}
}