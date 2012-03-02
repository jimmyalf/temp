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
		public virtual EyeParameter<Article> Article { get; set; }
		public virtual LensRecipe LensRecipe { get; set; }
        public virtual OrderShippingOption ShippingType { get; set; }
		public virtual PaymentOption SelectedPaymentOption { get; set; }
		public virtual DateTime Created { get; protected set; }
    	public virtual OrderCustomer Customer { get; set; }
		public virtual SubscriptionItem SubscriptionPayment { get; set; }
		public virtual decimal OrderTotalWithdrawalAmount { get; set; }
        public virtual int? SpinitServicesEmailId { get; set; }
		public virtual OrderStatus Status { get; set; }
    	public virtual string Reference { get; set; }
    }
}