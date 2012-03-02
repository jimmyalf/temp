using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

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
            BaseCurveOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };
            DiameterOptions = new List<ListItem> { new ListItem { Text = "-- Välj --", Value = DefaultOptionValue.ToString() } };

			SelectedBaseCurve = new EyeParameter<decimal>(DefaultOptionValue, DefaultOptionValue);
			SelectedDiameter = new EyeParameter<decimal>(DefaultOptionValue, DefaultOptionValue);
			SelectedPower = new EyeParameter<string>();
			SelectedCylinder = new EyeParameter<string>();
			SelectedAddition = new EyeParameter<string>();
			SelectedAxis = new EyeParameter<string>();
		}

        public string CustomerName { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedArticleTypeId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedArticleId { get; set; }
        public int SelectedShippingOption { get; set; }

		public EyeParameter<string> SelectedPower { get; set; }
		public EyeParameter<string> SelectedCylinder { get; set; }
		public EyeParameter<string> SelectedAddition { get; set; }
		public EyeParameter<string> SelectedAxis { get; set; }
		public EyeParameter<decimal> SelectedBaseCurve { get; set; }
		public EyeParameter<decimal> SelectedDiameter { get; set; }

		public IEnumerable<ListItem> Categories { get; set; }
    	public IEnumerable<ListItem> Suppliers { get; set; }
    	public IEnumerable<ListItem> ArticleTypes { get; set; }
		public IEnumerable<ListItem> ShippingOptions { get; set; }
        public IEnumerable<ListItem> OrderArticles { get; set; }
        public IEnumerable<ListItem> BaseCurveOptions { get; set; }
        public IEnumerable<ListItem> DiameterOptions { get; set; }

		public bool AxisOptionsEnabled { get; set; }
        public bool CylinderOptionsEnabled { get; set; }
        public bool AdditionOptionsEnabled { get; set; }
    	public string Reference { get; set; }
    }
}