using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class WpcGridDeleteFormExtensions
	{
		public static WpcGridDeleteForm WpcGridDeleteForm(this HtmlHelper helper, string actionName, string controllerName, object routeValues)
		{
			return new WpcGridDeleteForm(helper, actionName, controllerName, routeValues);
		}
		public static WpcGridDeleteForm WpcGridDeleteForm(this HtmlHelper helper, object rowItemModel, string actionName, string controllerName, object routeValues)
		{
			return new WpcGridDeleteForm(helper, rowItemModel, actionName, controllerName, routeValues);
		}
	}
}