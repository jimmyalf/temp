using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class ArticleTypeListView : CommonListView<ArticleTypeListItem, ArticleType>
	{
		public ArticleTypeListView() { }
		public ArticleTypeListView(string search, IEnumerable<ArticleType> types) : base(types,search) { }

		public override ArticleTypeListItem Convert(ArticleType item)
		{
			return new ArticleTypeListItem
			{
				ArticleTypeId = item.Id.ToString(), 
				Name = item.Name,
				CategoryName = item.Category.Name
			};
		}
	}
	public class ArticleTypeListItem
	{
		public string ArticleTypeId { get; set; }
		public string Name { get; set; }
		public string CategoryName { get; set; }
	}
}