using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class ArticleTypeFormView : CommonFormView
	{
		public ArticleTypeFormView() { }
		public ArticleTypeFormView(IEnumerable<ArticleCategory> categories, int? id = null, string name = null) : base(id)
		{
			Name = name;
			SetArticleCategories(categories);
		}

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