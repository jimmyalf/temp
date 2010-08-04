using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class PageOfFramesMatchingCriteria : PagedCriteria, IActionCriteria
	{
		public string NameLike { get; set; }
		public bool SortAscending { get; set; }
		public string OrderBy { get; set; }
	}
}