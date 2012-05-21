using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Spinit.Wpc.Synologen.Presentation.Models.Order;

namespace Spinit.Wpc.Synologen.Presentation.Application
{
	public static class MvcExtensions
	{
		public static string GetRoute(this RouteCollection routeCollection, string controller, string action)
		{
			return GetRoute(routeCollection, controller, action, null);
		}

		public static string GetRoute(this RouteCollection routeCollection, string controller, string action, RouteValueDictionary additionalRouteData)
		{
			var routeValueDictionary = new RouteValueDictionary {{"action", action}, {"controller", controller}};
			if (additionalRouteData != null)
			{
				foreach (var routeItem in additionalRouteData)
				{
					routeValueDictionary.Add(routeItem.Key, routeItem.Value);
				}
			}
			var virtualPath = routeCollection.GetVirtualPath(null, routeValueDictionary);
			if (virtualPath == null) throw new ApplicationException("Unable to find specified route. \r\nController: {ControllerName}, \r\nAction: {ActionName}".Replace("{ControllerName}", controller).Replace("{ActionName}", action));
			return virtualPath.VirtualPath;

		}

		public static MvcHtmlString GetDisplayName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
			return MvcHtmlString.Create(value);
		}

		public static void AddCustomValidationErrors(this ModelStateDictionary dictionary, IEnumerable<ValidationError> validationErrors)
		{
			foreach (var validationError in validationErrors)
			{
				dictionary.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
			}
		}
	}
}