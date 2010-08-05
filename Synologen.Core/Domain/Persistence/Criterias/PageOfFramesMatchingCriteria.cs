using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class PageOfFramesMatchingCriteria : PagedSortedCriteria, IActionCriteria
	{
		public string NameLike { get; set; }
	}
}