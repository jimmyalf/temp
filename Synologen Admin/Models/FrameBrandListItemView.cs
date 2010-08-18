using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameBrandListItemView : IDeleConfigurableGridItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int NumberOfFramesWithThisBrand { get; set; }
		public bool AllowDelete { get { return (NumberOfFramesWithThisBrand <= 0); } }
	}
}