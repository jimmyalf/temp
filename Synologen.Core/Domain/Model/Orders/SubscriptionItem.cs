using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionItem : Entity
	{
		public SubscriptionItem()
		{
			CreatedDate = SystemTime.Now;
			Version = SubscriptionVersion.VersionTwo;
		    Active = true;
		}

		public virtual DateTime CreatedDate { get; private set; }
		public virtual Subscription Subscription { get; set; }
		public virtual int? WithdrawalsLimit { get; protected set; }
		public virtual int PerformedWithdrawals { get; set; }
		public virtual SubscriptionAmount Value { get; protected set; }
		public virtual SubscriptionVersion Version { get; protected set; }
		public virtual bool IsOngoing { get { return !WithdrawalsLimit.HasValue; } }
		public virtual SubscriptionAmount MonthlyWithdrawal
		{
			get
			{
				var taxedAmount = IsOngoing ? CustomMonthlyAmount.Taxed : WithdrawalsLimit <= 0 ? 0 : Math.Round(Value.Taxed / WithdrawalsLimit.Value, 2);
				var taxFreeAmount = IsOngoing ? CustomMonthlyAmount.TaxFree : WithdrawalsLimit <= 0 ? 0 : Math.Round(Value.TaxFree / WithdrawalsLimit.Value, 2);
				return new SubscriptionAmount(taxedAmount, taxFreeAmount);
			}
		}
	    public virtual SubscriptionItemStatus Status
	    {
	        get
	        {
	            if (!Active)
	            {
	                return SubscriptionItemStatus.Stopped;
	            }

                if (IsOngoing)
                {
                    return SubscriptionItemStatus.Active;
                }

                return PerformedWithdrawals < WithdrawalsLimit 
                    ? SubscriptionItemStatus.Active 
                    : SubscriptionItemStatus.Expired;
	        }
	    }
        protected virtual SubscriptionAmount CustomMonthlyAmount { get; set; }
        protected virtual bool Active { get; set; }

        public virtual SubscriptionItem Start()
        {
            Active = true;
            return this;
        }

        public virtual SubscriptionItem Stop()
        {
            Active = false;
            return this;
        }

        public virtual SubscriptionItem Setup(int withdrawalLimit, decimal totalPrice, decimal totalFee)
        {
            WithdrawalsLimit = withdrawalLimit;
            Value = new SubscriptionAmount(totalPrice, totalFee);
            CustomMonthlyAmount = null;
            return this;
        }

        public virtual SubscriptionItem Setup(decimal monthlyPrice, decimal monthlyFee, decimal totalPrice, decimal totalFee)
        {
            WithdrawalsLimit = null;
            Value = new SubscriptionAmount(totalPrice, totalFee);
            CustomMonthlyAmount = new SubscriptionAmount(monthlyPrice, monthlyFee);
            return this;
        }
	}
}