using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{

    public class CreateOrderEventArgs : EventArgs
    {
        public int ShipmentOption { get; set; }
        public int CustomerId { get; set; }
        public int ArticleId { get; set; }

        public float LeftPower { get; set; }
        public float RightPower { get; set; }
        public float LeftDiameter { get; set; }
        public float RightDiameter { get; set; }
        public float LeftBaseCurve { get; set; }
        public float RightBaseCurve { get; set; }        
        public float LeftAxis { get; set; }
        public float RightAxis { get; set; }
        public float LeftCylinder { get; set; }
        public float RightCylinder { get; set; }
        public float LeftAddition { get; set; }
        public float RightAddition { get; set; }
    }
}