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

            PowerOptions= Enumerable.Empty<ListItem>();
            BaseCurveOptions = Enumerable.Empty<ListItem>();
            DiameterOptions= Enumerable.Empty<ListItem>();
            AxisOptions = Enumerable.Empty<ListItem>();
            CylinderOptions = Enumerable.Empty<ListItem>();

    	}
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int SelectedCategoryId { get; set; }
        public int SelectedArticleTypeId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedArticleId { get; set; }

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