using MvcContrib.UI.Grid;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class GridPageSortParameters : GridSortOptions
	{
		public GridPageSortParameters()
		{
			Page = 1;
		}
		public int Page { get; set; }
		public int? PageSize { get; set; }
	}
}