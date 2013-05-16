using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
    public class PaymentOptionsModel
    {
    	public PaymentOptionsModel()
    	{
    		Subscriptions = Enumerable.Empty<ListItem>();
    	}

		public IEnumerable<ListItem> Subscriptions { get; set; }
    	public string CustomerName { get; set; }
        public int SelectedOption { get; set; }
    }
}