using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class FrameFormEventArgs : System.EventArgs
	{
		public EyeParameter SelectedPupillaryDistance { get; set; }
		public EyeParameter SelectedSphere { get; set; }
		public EyeParameter SelectedCylinder { get; set; }
		public int SelectedFrameId { get; set; }
		public int SelectedGlassTypeId { get; set; }

		//public decimal SelectedPupillaryDistanceLeft { get; set; }
		//public decimal SelectedPupillaryDistanceRight { get; set; }

		//public decimal SelectedSphereLeft { get; set; }
		//public decimal SelectedSphereRight { get; set; }

		public bool PageIsValid { get; set; }
	}
}