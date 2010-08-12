using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models
{
	public class FrameOrderModel
	{
		public string Message { get; set; }
		
		public IEnumerable<FrameListItem> FramesList { get; set; }
		public IEnumerable<IntervalListItem> IndexList { get; set; }
		public IEnumerable<IntervalListItem> SphereList { get; set; }

		public string FrameRequiredErrorMessage { get; set; }
		public string IndexRequiredErrorMessage { get; set; }
		public string SphereRequiredErrorMessage { get; set; }

		public int SelectedFrameId { get; set; }

		public int NotSelectedIntervalValue { get { return int.MinValue; } }

		
	}
}