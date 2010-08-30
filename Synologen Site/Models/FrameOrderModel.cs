using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models
{
	public class FrameOrderModel
	{
		public IEnumerable<FrameListItem> FramesList { get; set; }
		public IEnumerable<FrameGlassTypeListItem> GlassTypesList { get; set; }
		public IEnumerable<IntervalListItem> PupillaryDistanceList { get; set; }
		public IEnumerable<IntervalListItem> SphereList { get; set; }
		public IEnumerable<IntervalListItem> CylinderList { get; set; }

		public string FrameRequiredErrorMessage { get; set; }
		public string PupillaryDistanceRequiredErrorMessage { get; set; }
		public string GlassTypeRequiredErrorMessage { get; set; }
		public string SphereRequiredErrorMessage { get; set; }
		public string CylinderRequiredErrorMessage { get; set; }

		public int SelectedFrameId { get; set; }
		public int SelectedGlassTypeId { get; set; }

		public decimal SelectedPupillaryDistanceLeft { get; set; }
		public decimal SelectedPupillaryDistanceRight { get; set; }

		public decimal SelectedSphereLeft { get; set; }
		public decimal SelectedSphereRight { get; set; }

		public int NotSelectedIntervalValue { get { return int.MinValue; } }

		public decimal SelectedCylinderLeft { get; set; }
		public decimal SelectedCylinderRight { get; set; }
	}
}