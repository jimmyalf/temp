using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class PageOfFramesMatchingCriteria : PagedCriteria, IActionCriteria
	{
		//public int IdGreaterThen { get; set; }
		public string NameLike { get; set; }
	}
}