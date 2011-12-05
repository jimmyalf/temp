using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class Order : Entity
    {
    	public Order()
    	{
    		Created = SystemClock.Now;
    	}
		public virtual Article Article { get; set; }
		public virtual LensRecipe LensRecipe { get; set; }
        public virtual OrderShippingOption ShippingType { get; set; }
		public virtual DateTime Created { get; protected set; }
    }

	public class LensRecipe : Entity
	{
		public virtual EyeParameter BaseCurve { get; set; }
		public virtual EyeParameter Diameter { get; set; }
		public virtual EyeParameter Power { get; set; }
	}

	public class EyeParameter
	{
		public virtual int Left { get; set; }
		public virtual int Right { get; set; }
	}
}