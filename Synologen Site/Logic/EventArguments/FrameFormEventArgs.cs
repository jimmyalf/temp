using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class FrameFormEventArgs : EventArgs
	{
		public EyeParameter SelectedPupillaryDistance { get; set; }
		public EyeParameter SelectedSphere { get; set; }
		public EyeParameter SelectedCylinder { get; set; }
		public EyeParameter SelectedAxis { get; set; }
		public EyeParameter SelectedAddition { get; set; }
		public EyeParameter SelectedHeight { get; set; }
		public int SelectedFrameId { get; set; }
		public int SelectedGlassTypeId { get; set; }
		public bool PageIsValid { get; set; }
	}
}