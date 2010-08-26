using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameGlassTypeListItemView : IDeleConfigurableGridItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IncludeAddition { get; set; }
		public bool IncludeHeight { get; set; }
		public int NumberOfOrdersWithThisGlassType { get; set; }
		public bool AllowDelete
		{
			get { return (NumberOfOrdersWithThisGlassType <= 0); }
		}
	}
}