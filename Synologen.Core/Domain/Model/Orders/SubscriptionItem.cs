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
		public virtual decimal ProductPrice { get; set; }
		public virtual decimal FeePrice { get; set; }
		public virtual decimal? MonthlyPrice { get; protected set; }
		public virtual decimal? MonthlyFee { get; protected set; }

		public virtual SubscriptionItem Setup(int withdrawalLimit)
		{
			WithdrawalsLimit = withdrawalLimit;
			MonthlyFee = null;
			MonthlyPrice = null;
			return this;
		}

		public virtual SubscriptionItem Setup(decimal monthlyPrice, decimal monthlyFee)
		{
			WithdrawalsLimit = null;
			MonthlyFee = monthlyFee;
			MonthlyPrice = monthlyPrice;
			return this;
		}


		public virtual decimal TotalValue { get { return ProductPrice + FeePrice; } }

		public virtual bool IsOngoing { get { return !WithdrawalsLimit.HasValue; } }

		public virtual decimal MonthlyWithdrawalProductAmount
		{
		    get
		    {
				if(IsOngoing) return MonthlyPrice.Value;
		    	return WithdrawalsLimit <= 0 ? 0 : Math.Round(ProductPrice / WithdrawalsLimit.Value, 2);
		    }
		}

		public virtual decimal MonthlyWithdrawalFeeAmount
		{
		    get
		    {
				if(IsOngoing) return MonthlyFee.Value;
		    	return WithdrawalsLimit <= 0 ? 0 : Math.Round(FeePrice / WithdrawalsLimit.Value, 2);
		    }
		}

		public virtual decimal MonthlyWithdrawalAmount
		{
		    get { return MonthlyWithdrawalProductAmount + MonthlyWithdrawalFeeAmount; }
		}

		public virtual bool IsActive
		{ 
			get { return PerformedWithdrawals < WithdrawalsLimit; } 
		}


	}
}