using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class AllFramesMatchingCriteria : IActionCriteria
	{
		public string NameLike { get; set; }
		//public int IdGreaterThen { get; set; }
	}
}