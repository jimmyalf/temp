using System.Collections.Generic;
using System.ComponentModel;
using Spinit.Wpc.Synologen.Presentation.Models.Deviation;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
	public class CategoryListView
	{
		public IEnumerable<CategoryListItemView> List { get; set; }
		[DisplayName("Filtrera")]
		public string SearchTerm { get; set; }
	}
}