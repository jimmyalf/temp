namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class GridPageSortParameters : IGridPageParameters, IGridSortParameters//: GridSortOptions
	{
		public GridPageSortParameters()
		{
			Page = 1;
			Direction = SortDirection.Ascending;
		}
		public int Page { get; set; }
		public int? PageSize { get; set; }
		public string Column { get; set; }
		public SortDirection Direction { get; set; } 
		//public bool SortAscending { get { return Direction == SortDirection.Ascending; } }
	}
	public interface IGridSortParameters
	{
		string Column { get; set; }
		SortDirection Direction { get; set; } 
	}
	public interface IGridPageParameters
	{
		int Page { get; set; }
		int? PageSize { get; set; }
	}
	public enum SortDirection
	{
		Ascending = 1,
		Descending = 2
	}
}