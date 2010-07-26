namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface IPagedCriteria
	{
		int Page { get; set; }
		int PageSize { get; set; }
	}
}