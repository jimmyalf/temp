using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Models.Order;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IOrderViewParser
	{
		ArticleCategory GetEntity(CategoryFormView viewModel, Func<int,ArticleCategory> getCategory);
		CategoryFormView GetCategoryFormView(int? id, Func<int,ArticleCategory> getArticle);

		ArticleType GetEntity(ArticleTypeFormView viewModel, Func<int, ArticleType> getArticleType, Func<int,ArticleCategory> getCategory);
		ArticleTypeFormView GetArticleTypeFormView(int? id, Func<int, ArticleType> getArticleType, Func<IEnumerable<ArticleCategory>> getCategories);
	}

	public class OrderViewParser : IOrderViewParser
	{
		public ArticleCategory GetEntity(CategoryFormView viewModel, Func<int,ArticleCategory> getArticle)
		{
			var category = GetStoredItemOrNew(viewModel.Id, getArticle);
			category.Name = viewModel.Name;
			return category;
		}

		public ArticleType GetEntity(ArticleTypeFormView viewModel, Func<int, ArticleType> getArticleType, Func<int,ArticleCategory> getCategory)
		{
			var articleType = GetStoredItemOrNew(viewModel.Id, getArticleType);
			var category = GetStoredItemOrNew(viewModel.CategoryId, getCategory);
			articleType.Name = viewModel.Name;
			articleType.Category = category;
			return articleType;
		}

		public CategoryFormView GetCategoryFormView(int? id, Func<int,ArticleCategory> getArticle)
		{
			var category = GetStoredItemOrNew(id, getArticle);
			return new CategoryFormView {Id = id, Name = category.Name};
		}

		public ArticleTypeFormView GetArticleTypeFormView(int? id, Func<int, ArticleType> getArticleType, Func<IEnumerable<ArticleCategory>> getCategories)
		{
			var articleType = GetStoredItemOrNew(id, getArticleType);
			var categories = getCategories();
			return new ArticleTypeFormView(categories, id, articleType.Name)
			{
				CategoryId = articleType.With(x => x.Category).Return(x => x.Id, default(int)),
			};
		}

		private static TType GetStoredItemOrNew<TType>(int? id, Func<int,TType> getitem) where TType : class, new()
		{
			return id.HasValue ? getitem(id.Value) : new TType();
		}
	}
}