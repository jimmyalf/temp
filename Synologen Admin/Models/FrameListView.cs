using System.ComponentModel;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameListView
	{
		public ISortedPagedList<FrameListItemView> List { get; set; }
		[DisplayName("Filtrera")]
		public string SearchTerm { get; set; }
	}
}