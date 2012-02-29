using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedSomethingEventArgs : EventArgs
    {
		public const int DefaultOptionValue = -9999;
        //public int ExistingOrderId { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedArticleTypeId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedArticleId { get; set; }
        public int SelectedShippingOption { get; set; }
		public EyeParameter<decimal> SelectedAxis { get; set; }
		public EyeParameter<decimal> SelectedAddition { get; set; }
		public EyeParameter<decimal> SelectedCylinder { get; set; }
		public EyeParameter<decimal> SelectedDiameter { get; set; }
		public EyeParameter<decimal> SelectedBaseCurve { get; set; }
		public EyeParameter<decimal> SelectedPower { get; set; }

        public SelectedSomethingEventArgs()
        {
        	SelectedAxis = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
			SelectedAddition = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
			SelectedCylinder = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
			SelectedDiameter = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
			SelectedBaseCurve = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
			SelectedPower = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
        }
    }
}