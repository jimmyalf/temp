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
		public virtual Article Article { get; set; }
		public virtual LensRecipe LensRecipe { get; set; }
        public virtual OrderShippingOption ShippingType { get; set; }
		public virtual PaymentOption SelectedPaymentOption { get; set; }
		public virtual DateTime Created { get; protected set; }
    	public virtual OrderCustomer Customer { get; set; }
    }

	public class LensRecipe : Entity
	{
	    public virtual Order Order { get; set; }
		public virtual EyeParameter BaseCurve { get; set; }
		public virtual EyeParameter Diameter { get; set; }
		public virtual EyeParameter Power { get; set; }
	    public virtual EyeParameter Axis { get; set; }
	    public virtual EyeParameter Cylinder { get; set; }
	}

	public class EyeParameter
	{
		public virtual float Left { get; set; }
		public virtual float Right { get; set; }
	}
}