using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameOrderListView : GridPageSortParameters
	{
		public string Search { get; set; }
		public ISortedPagedList<FrameOrderListItemView> List { get; set; }
	}
}