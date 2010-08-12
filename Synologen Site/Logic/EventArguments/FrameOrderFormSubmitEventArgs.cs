using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class FrameOrderFormSubmitEventArgs : EventArgs
	{
		public bool PageIsValid { get; set; }
		public int SelectedFrameId { get; set; }
		public decimal SelectedIndex { get; set; }
		public decimal SelectedSphere { get; set; }
	}
}