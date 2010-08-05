using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameColorListView
	{
		

		[Required(ErrorMessage = "Namn måste anges")]
		[DisplayName("Bågfärg")]
		public string SelectedFrameColorName { get; set; }
		public int SelectedFrameColorId { get; set; }

		public ISortedPagedList<FrameColorListItemView> List { get; set; }
	}
}