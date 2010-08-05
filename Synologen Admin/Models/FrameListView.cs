using System.ComponentModel;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameListView
	{
		public ISortedPagedList<FrameListItemView> List { get; set; }
		[DisplayName("Filtrera")]
		public string SearchWord { get; set; }
	}
}