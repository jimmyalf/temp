using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class CreateOrderModel
    {
        public CreateOrderModel()
    	{
    	    
            Categories        = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
    		Suppliers         = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
            ArticleTypes      = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
            OrderArticles     = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
            ShippingOptions   = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };

            PowerOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };
            BaseCurveOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };
            DiameterOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };
            AxisOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };
            CylinderOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };
            AdditionOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = (-9999).ToString() } };

            SelectedLeftPower  = -9999;
            SelectedLeftBaseCurve  = -9999;
            SelectedLeftDiameter  = -9999;
            SelectedLeftCylinder  = -9999;
            SelectedLeftAxis  = -9999;
            SelectedLeftAddition  = -9999;
            SelectedRightPower  = -9999;
            SelectedRightBaseCurve  = -9999;
            SelectedRightDiameter  = -9999;
            SelectedRightCylinder  = -9999;
            SelectedRightAxis  = -9999;
            SelectedRightAddition  = -9999;            

            PowerOptionsEnabled = false;
            BaseCurveOptionsEnabled = false;
            AxisOptionsEnabled = false;
            DiameterOptionsEnabled = false;
            CylinderOptionsEnabled = false;
            AdditionOptionsEnabled = false;

    	}
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

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
        public float SelectedRightAxis { get; set; }
        public float SelectedRightAddition { get; set; }

		public IEnumerable<ListItem> Categories { get; set; }
    	public IEnumerable<ListItem> Suppliers { get; set; }
    	public IEnumerable<ListItem> ArticleTypes { get; set; }
		public IEnumerable<ListItem> ShippingOptions { get; set; }
        public IEnumerable<ListItem> OrderArticles { get; set; }

        public IEnumerable<ListItem> PowerOptions { get; set; }
        public IEnumerable<ListItem> BaseCurveOptions { get; set; }
        public IEnumerable<ListItem> DiameterOptions { get; set; }
        public IEnumerable<ListItem> AxisOptions { get; set; }
        public IEnumerable<ListItem> CylinderOptions { get; set; }
        public IEnumerable<ListItem> AdditionOptions { get; set; }
        public IEnumerable<ListItem> ItemQuantityOptions { get; set; }

        public bool PowerOptionsEnabled { get; set; }
        public bool BaseCurveOptionsEnabled { get; set; }
        public bool AxisOptionsEnabled { get; set; }
        public bool DiameterOptionsEnabled { get; set; }
        public bool CylinderOptionsEnabled { get; set; }
        public bool AdditionOptionsEnabled { get; set; }
    }
}