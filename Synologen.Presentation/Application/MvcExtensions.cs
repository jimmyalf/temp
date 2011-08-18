using System;
using System.Web.Routing;

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
			var routeValueDictionary = new RouteValueDictionary{{"action", action}, {"controller", controller}};
			if(additionalRouteData != null)
			{
				foreach (var routeItem in additionalRouteData)
				{
					routeValueDictionary.Add(routeItem.Key, routeItem.Value);
				}
			}
			var virtualPath = routeCollection.GetVirtualPath(null, routeValueDictionary);
			if(virtualPath == null) throw new ApplicationException(
				"Unable to find specified route. \r\nController: {ControllerName}, \r\nAction: {ActionName}"
				.Replace("{ControllerName}", controller)
				.Replace("{ActionName}", action)
			);
			return virtualPath.VirtualPath;
		}
	}
}