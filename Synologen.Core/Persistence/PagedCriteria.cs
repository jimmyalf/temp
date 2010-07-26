namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public class PagedCriteria : IPagedCriteria {
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}