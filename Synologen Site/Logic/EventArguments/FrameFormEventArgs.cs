namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class FrameFormEventArgs : System.EventArgs
	{
		public int SelectedFrameId { get; set; }
		public int SelectedGlassTypeId { get; set; }

		public decimal SelectedPupillaryDistanceLeft { get; set; }
		public decimal SelectedPupillaryDistanceRight { get; set; }

		public bool PageIsValid { get; set; }
	}
}