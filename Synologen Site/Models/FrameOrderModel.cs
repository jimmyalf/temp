using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models
{
	public class FrameOrderModel
	{
		public string Message { get; set; }
		
		public IEnumerable<FrameListItem> FramesList { get; set; }
		public IEnumerable<IntervalListItem> PupillaryDistanceList { get; set; }

		public string FrameRequiredErrorMessage { get; set; }
		public string PupillaryDistanceRequiredErrorMessage { get; set; }
		public string GlassTypeRequiredErrorMessage { get; set; }

		public int SelectedFrameId { get; set; }
		public decimal SelectedPupillaryDistanceLeft { get; set; }
		public decimal SelectedPupillaryDistanceRight { get; set; }

		public int NotSelectedIntervalValue { get { return int.MinValue; } }

		public IEnumerable<FrameGlassTypeListItem> GlassTypesList { get; set; }

		
	}
}