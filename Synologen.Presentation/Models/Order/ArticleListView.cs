using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class ArticleListView : CommonListView<ArticleListItem, Article>
	{
		public ArticleListView() { }
		public ArticleListView(string search, IEnumerable<Article> articles) : base(articles,search) { }

		public override ArticleListItem Convert(Article item)
		{
			return new ArticleListItem
			{
				ArticleId = item.Id.ToString(), 
				Name = item.Name,
				Supplier = item.ArticleSupplier.Name,
				Type = item.ArticleType.Name
			};
		}
	}

	public class ArticleListItem
	{
		public string ArticleId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Supplier { get; set; }
	}
}