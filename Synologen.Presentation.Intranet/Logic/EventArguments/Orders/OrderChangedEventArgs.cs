using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class OrderChangedEventArgs : EventArgs
    {
		//public const int DefaultOptionValue = -9999;
        public int? SelectedCategoryId { get; set; }
        public int? SelectedArticleTypeId { get; set; }
        public int? SelectedSupplierId { get; set; }
        public int? SelectedShippingOption { get; set; }
		public EyeParameter<int?> SelectedArticleId { get; set; }
		public EyeParameter<string> SelectedPower { get; set; }
		public EyeParameter<string> SelectedAddition { get; set; }
		public EyeParameter<string> SelectedAxis { get; set; }
		public EyeParameter<string> SelectedCylinder { get; set; }
		public EyeParameter<decimal?> SelectedDiameter { get; set; }
		public EyeParameter<decimal?> SelectedBaseCurve { get; set; }
		public EyeParameter<string> SelectedQuantity { get; set; }
		public EyeParameter<bool> OnlyUse { get; set; }
		public string SelectedReference { get; set; }
    	

    	public OrderChangedEventArgs()
        {
			SelectedArticleId = new EyeParameter<int?>();
			SelectedPower = new EyeParameter<string>();
			SelectedAddition = new EyeParameter<string>();
			SelectedAxis = new EyeParameter<string>();
			SelectedCylinder = new EyeParameter<string>();
    		SelectedQuantity = new EyeParameter<string>();
			SelectedDiameter = new EyeParameter<decimal?>();
			SelectedBaseCurve = new EyeParameter<decimal?>();
    		OnlyUse = new EyeParameter<bool>();
        }
    }
}