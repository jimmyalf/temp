using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameListItemView : IDeleConfigurableGridItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ArticleNumber { get; set; }
		public bool AllowOrders { get; set; }
		public string Color { get; set; }
		public string Brand { get; set; }
        public string Supplier { get; set; }
		public int NumberOfOrdersWithThisFrame { get; set; }
		public bool AllowDelete
		{
			get { return (NumberOfOrdersWithThisFrame <= 0); }
		}
	}
}