using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedSomethingEventArgs : EventArgs
    {
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
        public float SelectedRightPower { get; set; }
        public float SelectedRightBaseCurve { get; set; }
        public float SelectedRightDiameter { get; set; }
        public float SelectedRightCylinder { get; set; }
        public float SelectedRightAxis { get; set; }

        public SelectedSomethingEventArgs(
            int selectedCategoryId = 0,
            int selectedArticleTypeId = 0,
            int selectedSupplierId = 0,
            int selectedArticleId = 0,
            int selectedShippingOption = 0,
            float selectedLeftPower = 0,
            float selectedLeftBaseCurve = 0,
            float selectedLeftDiameter = 0,
            float selectedLeftCylinder = 0,
            float selectedLeftAxis = 0,
            float selectedRightPower = 0,
            float selectedRightBaseCurve = 0,
            float selectedRightDiameter = 0,
            float selectedRightCylinder = 0,
            float selectedRightAxis = 0)
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
            SelectedRightPower = selectedRightPower;
            SelectedRightBaseCurve = selectedRightBaseCurve;
            SelectedRightDiameter = selectedRightDiameter;
            SelectedRightCylinder = selectedRightCylinder;
            SelectedRightAxis = selectedRightAxis;
        }
    }
}