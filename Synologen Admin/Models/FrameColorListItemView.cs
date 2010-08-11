namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameColorListItemView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int NumberOfFramesWithThisColor { get; set; }
		public bool DisableDelete { get { return NumberOfFramesWithThisColor > 0; } }
	}
}