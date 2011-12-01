using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{

    public class CreateOrderEventArgs : EventArgs
    {
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public int TypeId { get; set; }
        public int ArticleId { get; set; }
        public int LeftPower { get; set; }
        public int RightPower { get; set; }
        public int LeftDiameter { get; set; }
        public int RightDiameter { get; set; }
        public int LeftBaseCurve { get; set; }
        public int RightBaseCurve { get; set; }
        public int ShipmentOption { get; set; }

        public int CustomerId { get; set; }
    }
}