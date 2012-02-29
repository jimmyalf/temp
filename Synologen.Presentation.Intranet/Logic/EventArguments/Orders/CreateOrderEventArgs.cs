using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{

    public class CreateOrderEventArgs : EventArgs
    {
        public int ShipmentOption { get; set; }
       // public int CustomerId { get; set; }
        public int ArticleId { get; set; }
        //public int ExistingOrderId { get; set; }
		//public decimal LeftPower { get; set; }
		//public decimal RightPower { get; set; }
		public EyeParameter<decimal?> Power { get; set; }
		//public decimal LeftDiameter { get; set; }
		//public decimal RightDiameter { get; set; }
		public EyeParameter<decimal?> Diameter { get; set; }
		//public decimal LeftBaseCurve { get; set; }
		//public decimal RightBaseCurve { get; set; }        
		public EyeParameter<decimal?> BaseCurve { get; set; }
		//public decimal LeftAxis { get; set; }
		//public decimal RightAxis { get; set; }
		public EyeParameter<decimal?> Axis { get; set; }
		//public decimal LeftCylinder { get; set; }
		//public decimal RightCylinder { get; set; }
		public EyeParameter<decimal?> Cylinder { get; set; }
		//public decimal LeftAddition { get; set; }
		//public decimal RightAddition { get; set; }
		public EyeParameter<decimal?> Addition { get; set; }

		//public EyeParameter<decimal?> GetEyeParameter(Func<CreateOrderEventArgs,decimal> left, Func<CreateOrderEventArgs,decimal> right, decimal defaultValue = -9999)
		//{
		//    var leftValue = left(this);
		//    var rightValue = right(this);
		//    return new EyeParameter<decimal?>
		//    {
		//        Left = leftValue.Equals(defaultValue) ? (decimal?) null : leftValue,
		//        Right = rightValue.Equals(defaultValue) ? (decimal?) null : rightValue
		//    };
		//}
    }
}