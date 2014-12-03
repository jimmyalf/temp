using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class Order : Entity
    {
    	public Order()
    	{
			Created = SystemTime.Now;
    		SelectedPaymentOption = new PaymentOption();
    		Status = OrderStatus.Created;
    	}

		public virtual Shop Shop { get; set; }
		public virtual LensRecipe LensRecipe { get; set; }
        public virtual OrderShippingOption ShippingType { get; set; }
		public virtual PaymentOption SelectedPaymentOption { get; set; }
		public virtual DateTime Created { get; protected set; }
    	public virtual OrderCustomer Customer { get; set; }
		public virtual SubscriptionItem SubscriptionPayment { get; set; }
		protected virtual decimal? OldWithdrawalAmount { get; set; }
		protected virtual SubscriptionAmount WithdrawalAmount { get; set; }

    	public virtual SubscriptionAmount GetWithdrawalAmount()
    	{
    		return OldWithdrawalAmount.HasValue ? new SubscriptionAmount(OldWithdrawalAmount.Value, 0) : WithdrawalAmount;
    	}
		public virtual void SetWithdrawalAmount(SubscriptionAmount amount)
		{
			WithdrawalAmount = amount;
		}

    	public virtual int? SpinitServicesEmailId { get; set; }
		public virtual OrderStatus Status { get; set; }
    	public virtual string Reference { get; set; }
    }
}