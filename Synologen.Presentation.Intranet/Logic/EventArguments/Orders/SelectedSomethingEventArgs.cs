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
        public decimal SelectedLeftPower { get; set; }
        public decimal SelectedLeftBaseCurve { get; set; }
        public decimal SelectedLeftDiameter { get; set; }
        public decimal SelectedLeftCylinder { get; set; }
        public decimal SelectedLeftAxis { get; set; }
        public decimal SelectedLeftAddition { get; set; }
        public decimal SelectedRightPower { get; set; }
        public decimal SelectedRightBaseCurve { get; set; }
        public decimal SelectedRightDiameter { get; set; }
        public decimal SelectedRightCylinder { get; set; }
        public decimal SelectedRightAddition { get; set; }
        public decimal SelectedRightAxis { get; set; }

        public SelectedSomethingEventArgs(
            int selectedCategoryId = 0,
            int selectedArticleTypeId = 0,
            int selectedSupplierId = 0,
            int selectedArticleId = 0,
            //int existingOrderId = 0,
            int selectedShippingOption = 0,
            decimal selectedLeftPower = DefaultOptionValue,
            decimal selectedLeftBaseCurve = DefaultOptionValue,
            decimal selectedLeftDiameter = DefaultOptionValue,
            decimal selectedLeftCylinder = DefaultOptionValue,
            decimal selectedLeftAxis = DefaultOptionValue,
            decimal selectedLeftAddition = DefaultOptionValue,
            decimal selectedRightPower = DefaultOptionValue,
            decimal selectedRightBaseCurve = DefaultOptionValue,
            decimal selectedRightDiameter = DefaultOptionValue,
            decimal selectedRightCylinder = DefaultOptionValue,
            decimal selectedRightAxis = DefaultOptionValue,
            decimal selectedRightAddition = DefaultOptionValue)
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