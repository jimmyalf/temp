namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public class PagedSortedCriteria : IPagedCriteria, ISortedCriteria {
		public int Page { get; set; }
		public int PageSize { get; set; }
		public bool SortAscending { get; set; }
		public string OrderBy { get; set; }
	}
}