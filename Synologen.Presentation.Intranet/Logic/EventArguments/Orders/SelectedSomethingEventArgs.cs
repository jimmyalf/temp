using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedSomethingEventArgs : EventArgs
    {
		public const int DefaultOptionValue = -9999;
        public int ExistingOrderId { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedArticleTypeId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedArticleId { get; set; }
        public int SelectedShippingOption { get; set; }
        public float SelectedLeftPower { get; set; }
        public float SelectedLeftBaseCurve { get; set; }
        public float SelectedLeftDiameter { get; set; }
        public float SelectedLeftCylinder { get; set; }
        public float SelectedLeftAxis { get; set; }
        public float SelectedLeftAddition { get; set; }
        public float SelectedRightPower { get; set; }
        public float SelectedRightBaseCurve { get; set; }
        public float SelectedRightDiameter { get; set; }
        public float SelectedRightCylinder { get; set; }
        public float SelectedRightAddition { get; set; }
        public float SelectedRightAxis { get; set; }

        public SelectedSomethingEventArgs(
            int selectedCategoryId = 0,
            int selectedArticleTypeId = 0,
            int selectedSupplierId = 0,
            int selectedArticleId = 0,
            //int existingOrderId = 0,
            int selectedShippingOption = 0,
            float selectedLeftPower = DefaultOptionValue,
            float selectedLeftBaseCurve = DefaultOptionValue,
            float selectedLeftDiameter = DefaultOptionValue,
            float selectedLeftCylinder = DefaultOptionValue,
            float selectedLeftAxis = DefaultOptionValue,
            float selectedLeftAddition = DefaultOptionValue,
            float selectedRightPower = DefaultOptionValue,
            float selectedRightBaseCurve = DefaultOptionValue,
            float selectedRightDiameter = DefaultOptionValue,
            float selectedRightCylinder = DefaultOptionValue,
            float selectedRightAxis = DefaultOptionValue,
            float selectedRightAddition = DefaultOptionValue)
        {
            SelectedCategoryId = selectedCategoryId;
            SelectedArticleTypeId = selectedArticleTypeId;
            SelectedSupplierId = selectedSupplierId;
            SelectedArticleId = selectedArticleId;
            SelectedShippingOption = selectedShippingOption;
            SelectedLeftPower = selectedLeftPower;
            SelectedLeftBaseCurve = selectedLeftBaseCurve;
            SelectedLeftDiameter = selectedLeftDiameter;
            SelectedLeftCylinder = selectedLeftCylinder;
            SelectedLeftAxis = selectedLeftAxis;
            SelectedLeftAddition = selectedLeftAddition;
            SelectedRightPower = selectedRightPower;
            SelectedRightBaseCurve = selectedRightBaseCurve;
            SelectedRightDiameter = selectedRightDiameter;
            SelectedRightCylinder = selectedRightCylinder;
            SelectedRightAxis = selectedRightAxis;
            SelectedRightAddition = selectedRightAddition;
            //ExistingOrderId = existingOrderId;
        }
    }
}