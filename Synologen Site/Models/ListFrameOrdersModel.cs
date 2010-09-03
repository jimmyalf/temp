using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models
{
	public class ListFrameOrdersModel
	{
		public IEnumerable<FrameOrderListItemModel> List { get; set; }
		public string ViewPageUrl { get; set; }
	}
}