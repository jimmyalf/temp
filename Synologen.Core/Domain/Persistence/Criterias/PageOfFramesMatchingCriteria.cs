using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class PageOfFramesMatchingCriteria : PagedSortedCriteria
	{
		public PageOfFramesMatchingCriteria() : base(typeof(Frame)) {}
		public string NameLike { get; set; }
	}
}