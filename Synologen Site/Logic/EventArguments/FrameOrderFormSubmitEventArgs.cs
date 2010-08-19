using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class FrameOrderFormSubmitEventArgs : EventArgs
	{
		public bool PageIsValid { get; set; }
		public int SelectedFrameId { get; set; }
		public decimal SelectedPupillaryDistanceLeft { get; set; }
		public decimal SelectedPupillaryDistanceRight { get; set; }
	}
}