using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class PageOfFrameOrdersMatchingCriteria : PagedSortedCriteria {
		public PageOfFrameOrdersMatchingCriteria() : base(typeof(FrameOrder)) {}
		public string Search { get; set; }
	}
}