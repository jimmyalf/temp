using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class Order : Entity
    {
    	public Order()
    	{
    		Created = SystemClock.Now;
    		SelectedPaymentOption = new PaymentOption();
    	}

		public virtual Shop Shop { get; set; }
		public virtual Article Article { get; set; }
		public virtual LensRecipe LensRecipe { get; set; }
        public virtual OrderShippingOption ShippingType { get; set; }
		public virtual PaymentOption SelectedPaymentOption { get; set; }
		public virtual DateTime Created { get; protected set; }
    	public virtual OrderCustomer Customer { get; set; }
		public virtual SubscriptionItem SubscriptionPayment { get; set; }
		public virtual decimal? AutoWithdrawalAmount { get; set; }
    }
}