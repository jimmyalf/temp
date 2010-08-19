using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class FrameSelectedEventArgs : EventArgs
	{
		public int SelectedFrameId { get; set; }

		public decimal SelectedPupillaryDistanceLeft { get; set; }

		public decimal SelectedPupillaryDistanceRight { get; set; }
	}
}