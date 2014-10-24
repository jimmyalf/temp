using System.Collections.Generic;
using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameListView
	{
		public IEnumerable<FrameListItemView> List { get; set; }
		[DisplayName("Filtrera")]
		public string SearchTerm { get; set; }
	}
}