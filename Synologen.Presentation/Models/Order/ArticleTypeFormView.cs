using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class ArticleTypeFormView : CommonFormView
	{
		public ArticleTypeFormView()
		{
			Active = true;
		}
		public ArticleTypeFormView(IEnumerable<ArticleCategory> categories, int? id, ArticleType articleType) : base(id)
		{
			Name = articleType.Name;
			Active = articleType.Active;
			SetArticleCategories(categories);
			CategoryId = articleType.With(x => x.Category).Return(x => x.Id, default(int));
		}

		[DisplayName("Aktiv")]
		public bool Active { get; set; }

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		[DisplayName("Artikelkategori"), Required]
		public int CategoryId { get; set; }

		public SelectList Categories { get; set; }

		public ArticleTypeFormView SetArticleCategories(IEnumerable<ArticleCategory> categories)
		{
			Categories = categories.ToSelectList(x => x.Id, x => x.Name);
			return this;
		}
	}
}