using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models
{
	public class ListFrameOrdersModel : FrameOrderBaseModel
	{
		public IEnumerable<FrameOrderListItemModel> List { get; set; }
		public string ViewPageUrl { get; set; }
		public bool DisplayList
		{
			get { return (ShopDoesNotHaveAccessToFrameOrders == false); }
		}
	}
}