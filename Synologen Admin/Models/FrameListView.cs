using System.ComponentModel;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameListView
	{
		public IPagedList<FrameListItemView> List { get; set; }
		[DisplayName("Filtrera")]
		public string SearchWord { get; set; }
	}

	public class FrameListItemView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ArticleNumber { get; set; }
		public string Color { get; set; }
		public string Brand { get; set; }
		public bool AllowOrders { get; set; }
	}
}