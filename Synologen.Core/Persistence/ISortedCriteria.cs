namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface ISortedCriteria
	{
		bool SortAscending { get; set; }
		string OrderBy { get; set; }		
	}
}