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
		}

		public virtual DateTime CreatedDate { get; private set; }
		public virtual Subscription Subscription { get; set; }
		public virtual int? WithdrawalsLimit { get; protected set; }
		public virtual int PerformedWithdrawals { get; set; }
		public virtual SubscriptionAmount Value { get; protected set; }
		protected virtual SubscriptionAmount CustomMonthlyAmount { get; set; }
		public virtual SubscriptionVersion Version { get; protected set; }

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

		public virtual bool IsActive
		{ 
			get { return IsOngoing || PerformedWithdrawals < WithdrawalsLimit; } 
		}
	}
}