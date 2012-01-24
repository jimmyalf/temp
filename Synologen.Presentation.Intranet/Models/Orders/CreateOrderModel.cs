using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class CreateOrderModel
    {
		public const int DefaultOptionValue = -9999;
        public CreateOrderModel()
    	{
    	    
            Categories        = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
    		Suppliers         = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
            ArticleTypes      = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
            OrderArticles     = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };
            ShippingOptions   = new List<ListItem> { new ListItem {Text = "-- Välj --", Value = 0.ToString()} };

            PowerOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };
            BaseCurveOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };
            DiameterOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };
            AxisOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };
            CylinderOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };
            AdditionOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };

            SelectedLeftPower  = DefaultOptionValue;
            SelectedLeftBaseCurve  = DefaultOptionValue;
            SelectedLeftDiameter  = DefaultOptionValue;
            SelectedLeftCylinder  = DefaultOptionValue;
            SelectedLeftAxis  = DefaultOptionValue;
            SelectedLeftAddition  = DefaultOptionValue;
            SelectedRightPower  = DefaultOptionValue;
            SelectedRightBaseCurve  = DefaultOptionValue;
            SelectedRightDiameter  = DefaultOptionValue;
            SelectedRightCylinder  = DefaultOptionValue;
            SelectedRightAxis  = DefaultOptionValue;
            SelectedRightAddition  = DefaultOptionValue;            

			//PowerOptionsEnabled = false;
			//BaseCurveOptionsEnabled = false;
			//AxisOptionsEnabled = false;
			//DiameterOptionsEnabled = false;
			//CylinderOptionsEnabled = false;
			//AdditionOptionsEnabled = false;
    
    	}

       // public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        //public int ExistingOrderId { get; set; }

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

        //public bool PowerOptionsEnabled { get { return PowerOptions.Any(item => item.Value != DefaultOptionValue.ToString()); } }
		//public bool BaseCurveOptionsEnabled { get { return BaseCurveOptions.Any(item => item.Value != DefaultOptionValue.ToString()); } }
		//public bool DiameterOptionsEnabled { get { return DiameterOptions.Any(item => item.Value != DefaultOptionValue.ToString()); }  }
		public bool AxisOptionsEnabled { get; set; }
        public bool CylinderOptionsEnabled { get; set; }
        public bool AdditionOptionsEnabled { get; set; }
    }
}