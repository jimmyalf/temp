using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class CreateOrderModel
    {
        public CreateOrderModel()
    	{
    	    
            Categories        = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} }; // = Enumerable.Empty<ListItem>();
    		Suppliers         = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} }; // = Enumerable.Empty<ListItem>();
			ArticleTypes      = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} }; // = Enumerable.Empty<ListItem>();
            OrderArticles     = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} }; // = Enumerable.Empty<ListItem>();
            ShippingOptions = Enumerable.Empty<ListItem>();

            PowerOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = 0.ToString() } };
            BaseCurveOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = 0.ToString() } };
            DiameterOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = 0.ToString() } };
            AxisOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = 0.ToString() } };
            CylinderOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = 0.ToString() } };

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
        public float SelectedRightPower { get; set; }
        public float SelectedRightBaseCurve { get; set; }
        public float SelectedRightDiameter { get; set; }
        public float SelectedRightCylinder { get; set; }
        public float SelectedRightAxis { get; set; }

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
        public IEnumerable<ListItem> ItemQuantityOptions { get; set; }
    }
}