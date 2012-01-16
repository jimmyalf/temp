using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class CategoryListView : CommonListView<CategoryListItem, ArticleCategory>
	{
		public CategoryListView() { }
		public CategoryListView(string search, IEnumerable<ArticleCategory> categories) 
			: base(categories,search) { }

		public override CategoryListItem Convert(ArticleCategory item)
		{
			return new CategoryListItem {CategoryId = item.Id.ToString(), Name = item.Name};
		}
	}

	public class CategoryListItem
	{
		public string CategoryId { get; set; }
		public string Name { get; set; }
	}
}