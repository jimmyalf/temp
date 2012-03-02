using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedSomethingEventArgs : EventArgs
    {
		public const int DefaultOptionValue = -9999;
        public int SelectedCategoryId { get; set; }
        public int SelectedArticleTypeId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedArticleId { get; set; }
        public int SelectedShippingOption { get; set; }
		public EyeParameter<string> SelectedPower { get; set; }
		public EyeParameter<string> SelectedAddition { get; set; }
		public EyeParameter<string> SelectedAxis { get; set; }
		public EyeParameter<string> SelectedCylinder { get; set; }
		public EyeParameter<decimal> SelectedDiameter { get; set; }
		public EyeParameter<decimal> SelectedBaseCurve { get; set; }
		public string SelectedReference { get; set; }
		

        public SelectedSomethingEventArgs()
        {
			SelectedPower = new EyeParameter<string>();
			SelectedAddition = new EyeParameter<string>();
			SelectedAxis = new EyeParameter<string>();
			SelectedCylinder = new EyeParameter<string>();
			SelectedDiameter = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
			SelectedBaseCurve = new EyeParameter<decimal> {Left = DefaultOptionValue, Right = DefaultOptionValue};
        }
    }
}