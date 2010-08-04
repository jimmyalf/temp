using System.ComponentModel;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.App;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameListView
	{
		public IPagedList<FrameListItemView> List { get; set; }
		[DisplayName("Filtrera")]
		public string SearchWord { get; set; }
		public GridSortOptions SortOptions { get; set; }
	}

	public class FrameListItemView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ArticleNumber { get; set; }
		public bool AllowOrders { get; set; }

		[ModelDomainMapping(DomainPropertyName = "Color.Name")]
		public string Color { get; set; }

		[ModelDomainMapping(DomainPropertyName = "Brand.Name")]
		public string Brand { get; set; }
	}
}