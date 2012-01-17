using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Models.Order;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IOrderViewParser
	{
		ArticleCategory GetEntity(CategoryFormView viewModel, Func<int,ArticleCategory> getArticle);
		CategoryFormView GetView(int? id, Func<int,ArticleCategory> getArticle);
	}

	public class OrderViewParser : IOrderViewParser
	{
		public ArticleCategory GetEntity(CategoryFormView viewModel, Func<int,ArticleCategory> getArticle)
		{
			var category = GetStoredItemOrNew(viewModel.Id, getArticle);
			category.Name = viewModel.Name;
			return category;
		}
		public CategoryFormView GetView(int? id, Func<int,ArticleCategory> getArticle)
		{
			var category = GetStoredItemOrNew(id, getArticle);
			return new CategoryFormView {Id = id, Name = category.Name};
		}

		private static TType GetStoredItemOrNew<TType>(int? id, Func<int,TType> getArticle) where TType : class, new()
		{
			return id.HasValue ? getArticle(id.Value) : new TType();
		}
	}
}