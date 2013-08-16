using System.Collections.Generic;
using System.ComponentModel;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameOrderListView : GridPageSortParameters
	{
		[DisplayName("Filtrera")]
		public string SearchTerm { get; set; }
		public IEnumerable<FrameOrderListItemView> List { get; set; }
	}
}