using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{

    public class CreateOrderEventArgs : EventArgs
    {
        public int ShipmentOption { get; set; }
        public int ArticleId { get; set; }
		public EyeParameter<string> Power { get; set; }
		public EyeParameter<decimal?> Diameter { get; set; }
		public EyeParameter<decimal?> BaseCurve { get; set; }
		public EyeParameter<string> Axis { get; set; }
		public EyeParameter<string> Cylinder { get; set; }
		public EyeParameter<string> Addition { get; set; }
    }
}