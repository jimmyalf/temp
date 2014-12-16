using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcGridDeleteForm
	{
		protected readonly HtmlHelper _helper;
		protected readonly string _actionName;
		protected readonly string _controllerName;
		protected readonly object _routeValues;
		protected readonly IDictionary<string, object> _submitHtmlAttributes;
		protected IDictionary<string, object> _submitOverrideHtmlAttributes;
		protected bool _hideDeleteForm;

		public WpcGridDeleteForm(HtmlHelper helper, string actionName, string controllerName, object routeValues) :
			this(helper, null, actionName, controllerName, routeValues){ }

		public WpcGridDeleteForm(HtmlHelper helper, object itemRowModel, string actionName, string controllerName, object routeValues)
		{
			_helper = helper;
			_actionName = actionName;
			_controllerName = controllerName;
			_routeValues = routeValues;
			_submitHtmlAttributes = new Dictionary<string, object>
			{
				{"type", "submit"},
				{"value", "Radera"},
				{"class", "btnSmall delete"},
				{"title", "Radera"}
			};
			_submitOverrideHtmlAttributes = new Dictionary<string, object>();

			if(itemRowModel is IDeleConfigurableGridItem)
			{
				_hideDeleteForm = !((IDeleConfigurableGridItem) itemRowModel).AllowDelete;
			}
		}

		public WpcGridDeleteForm OverrideButtonAttributes(IDictionary<string, object> attributes)
		{
			_submitOverrideHtmlAttributes = attributes;
			return this;
		}

		public WpcGridDeleteForm OverrideButtonAttributes(params Expression<Func<string, object>>[] hash)
		{
		    _submitOverrideHtmlAttributes = ConvertHashToDictionary(hash);
		    return this;
		}

		public WpcGridDeleteForm HideDeleteFormIf(bool hideDeleteForm)
		{
			_hideDeleteForm = hideDeleteForm;
		    return this;
		}

		public override string ToString() {
			return RenderForm();
		}

		#region Rendering

		protected string RenderForm()
		{
			if(_hideDeleteForm) return String.Empty;
			var returnValue = new StringBuilder();
			returnValue.Append(RenderStartForm());
			returnValue.Append(RenderAntiForgeryToken());
			returnValue.Append(RenderSubmitInput());
			returnValue.Append(RenderEndForm());
			return returnValue.ToString();
		}

		protected virtual string RenderStartTd()
		{
			return "<td class=\"center\">";
		}

		protected virtual string RenderEndTd()
		{
			return "</td>";
		}

		protected virtual string RenderStartForm()
		{
			var formAction = UrlHelper.GenerateUrl(null, _actionName, _controllerName, new RouteValueDictionary(_routeValues), _helper.RouteCollection, _helper.ViewContext.RequestContext, true);
            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttribute("action", formAction);
            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(FormMethod.Post), true);
			return tagBuilder.ToString(TagRenderMode.StartTag);
		}

		protected virtual string RenderEndForm()
		{
			return "</form>";
		}

		protected virtual string RenderAntiForgeryToken()
		{
			return _helper.AntiForgeryToken().ToHtmlString();
		}

		protected virtual string RenderSubmitInput()
		{
			var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(_submitHtmlAttributes);
			tagBuilder.MergeAttributes(_submitOverrideHtmlAttributes, true);
			return tagBuilder.ToString();
		}

		#endregion

		protected static IDictionary<string, T> ConvertHashToDictionary<T>(IEnumerable<Expression<Func<string, T>>> hash)
		{
			IDictionary<string, T> returnValue = new Dictionary<string, T>();
			foreach (var func in hash)
			{
				var value = func.Compile().Invoke(null);
				var key = func.Parameters[0].Name;
				returnValue.Add(key, value);
			}
			return returnValue;
		}

	}
}