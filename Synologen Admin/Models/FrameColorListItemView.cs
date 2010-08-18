using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameColorListItemView : IDeleConfigurableGridItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int NumberOfFramesWithThisColor { get; set; }
		public bool AllowDelete
		{
			get { return (NumberOfFramesWithThisColor <= 0); }
		}
	}
}