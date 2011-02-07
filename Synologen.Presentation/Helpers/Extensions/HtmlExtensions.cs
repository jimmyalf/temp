using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class HtmlExtensions
	{
		public static MvcHtmlString GetDisplayNameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
			return MvcHtmlString.Create(value);
		}
	}
}