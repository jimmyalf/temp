using System;
using System.Collections.Generic;
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

		ArticleSupplier GetEntity(SupplierFormView viewModel, Func<int, ArticleSupplier> getSupplier);
		SupplierFormView GetSupplierFormView(int? id, Func<int, ArticleSupplier> getSupplier);
	}

	public class OrderViewParser : IOrderViewParser
	{
		public ArticleCategory GetEntity(CategoryFormView viewModel, Func<int,ArticleCategory> getArticle)
		{
			var category = GetStoredItemOrNew(viewModel.Id, getArticle);
			category.Name = viewModel.Name;
			category.Active = viewModel.Active;
			return category;
		}
		public ArticleType GetEntity(ArticleTypeFormView viewModel, Func<int, ArticleType> getArticleType, Func<int,ArticleCategory> getCategory)
		{
			var articleType = GetStoredItemOrNew(viewModel.Id, getArticleType);
			var category = GetStoredItemOrNew(viewModel.CategoryId, getCategory);
			articleType.Name = viewModel.Name;
			articleType.Category = category;
			articleType.Active = viewModel.Active;
			return articleType;
		}
		public ArticleSupplier GetEntity(SupplierFormView viewModel, Func<int, ArticleSupplier> getSupplier)
		{
			var supplier = GetStoredItemOrNew(viewModel.Id, getSupplier);
			supplier.Name = viewModel.Name;
			supplier.ShippingOptions = viewModel.GetShippingOptions();
			supplier.OrderEmailAddress = viewModel.OrderEmailAddress;
			supplier.Active = viewModel.Active;
			return supplier;
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
			article.Active = viewModel.Active;
			return article;
		}

		public CategoryFormView GetCategoryFormView(int? id, Func<int,ArticleCategory> getArticle)
		{
			var category = GetStoredItemOrNew(id, getArticle);
			return new CategoryFormView(id, category);
		}
		public ArticleTypeFormView GetArticleTypeFormView(int? id, Func<int, ArticleType> getArticleType, Func<IEnumerable<ArticleCategory>> getCategories)
		{
			var articleType = GetStoredItemOrNew(id, getArticleType);
			var categories = getCategories();
			return new ArticleTypeFormView(categories, id, articleType);
		}
		public ArticleFormView GetArticleFormView(int? id, Func<int, Article> getArticle, Func<IEnumerable<ArticleSupplier>> getSuppliers, Func<IEnumerable<ArticleType>> getTypes)
		{
			var article = GetStoredItemOrNew(id, getArticle);
			var suppliers = getSuppliers();
			var types = getTypes();
			return new ArticleFormView(suppliers, types, id, article);
		}
		public SupplierFormView GetSupplierFormView(int? id, Func<int, ArticleSupplier> getSupplier)
		{
			var supplier = GetStoredItemOrNew(id, getSupplier);
			return new SupplierFormView(id, supplier);
		}

		private static TType GetStoredItemOrNew<TType>(int? id, Func<int,TType> getitem) where TType : class, new()
		{
			return id.HasValue ? getitem(id.Value) : new TType();
		}
	}
}