using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class CreateOrderModel
    {
		public static int DefaultOptionValue = -9999;
        public CreateOrderModel()
    	{
    	    
            Categories        = new List<ListItem> { new ListItem {Text = "-- V�lj --", Value = 0.ToString()} };
    		Suppliers         = new List<ListItem> { new ListItem {Text = "-- V�lj --", Value = 0.ToString()} };
            ArticleTypes      = new List<ListItem> { new ListItem {Text = "-- V�lj --", Value = 0.ToString()} };
            OrderArticles     = new List<ListItem> { new ListItem {Text = "-- V�lj --", Value = 0.ToString()} };
            ShippingOptions   = new List<ListItem> { new ListItem {Text = "-- V�lj --", Value = 0.ToString()} };

            PowerOptions = new List<ListItem> { new ListItem { Text = "-- V�lj --", Value = DefaultOptionValue.ToString() } };
            BaseCurveOptions = new List<ListItem> { new ListItem { Text = "-- V�lj --", Value = DefaultOptionValue.ToString() } };
            DiameterOptions = new List<ListItem> { new ListItem { Text = "-- V�lj --", Value = DefaultOptionValue.ToString() } };
            AxisOptions = new List<ListItem> { new ListItem { Text = "-- V�lj --", Value = DefaultOptionValue.ToString() } };
            CylinderOptions = new List<ListItem> { new ListItem { Text = "-- V�lj --", Value = DefaultOptionValue.ToString() } };
            AdditionOptions = new List<ListItem> { new ListItem { Text = "-- V�lj --", Value = DefaultOptionValue.ToString() } };

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
        public decimal SelectedRightAxis { get; set; }
        public decimal SelectedRightAddition { get; set; }

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