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

		Article GetEntity(ArticleFormView viewModel, Func<int, Article> getArticle, Func<int,ArticleSupplier> getSupplier, Func<int,ArticleType> getType);
		ArticleFormView GetArticleFormView(int? id, Func<int, Article> getArticle, Func<IEnumerable<ArticleSupplier>> getSuppliers, Func<IEnumerable<ArticleType>> getTypes);
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

		public Article GetEntity(ArticleFormView viewModel, Func<int, Article> getArticle, Func<int,ArticleSupplier> getSupplier, Func<int,ArticleType> getType)
		{
			var article = GetStoredItemOrNew(viewModel.Id, getArticle);
			var supplier = GetStoredItemOrNew(viewModel.SupplierId, getSupplier);
			var type = GetStoredItemOrNew(viewModel.TypeId, getType);
			article.ArticleSupplier = supplier;
			article.ArticleType = type;
			article.Name = viewModel.Name;
			article.Options = viewModel.GetArticleOptions();
			return article;
		}

		public ArticleFormView GetArticleFormView(int? id, Func<int, Article> getArticle, Func<IEnumerable<ArticleSupplier>> getSuppliers, Func<IEnumerable<ArticleType>> getTypes)
		{
			var article = GetStoredItemOrNew(id, getArticle);
			var suppliers = getSuppliers();
			var types = getTypes();
			return new ArticleFormView(suppliers, types, id, article.Name, article.Options)
			{
				SupplierId = article.With(x => x.ArticleSupplier).Return(x => x.Id, default(int)),
				TypeId = article.With(x => x.ArticleType).Return(x => x.Id, default(int)),
			};
		}

		private static TType GetStoredItemOrNew<TType>(int? id, Func<int,TType> getitem) where TType : class, new()
		{
			return id.HasValue ? getitem(id.Value) : new TType();
		}
	}
}