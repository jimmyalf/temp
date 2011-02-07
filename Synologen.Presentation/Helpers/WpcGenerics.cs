using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public static class WpcGenerics
	{
		public static string RenderCheckboxFor<TViewModel>(this TViewModel viewModel, Func<TViewModel,bool> viewModelProperty)
		{
			return RenderCheckboxFor(viewModel, viewModelProperty, null, null);
		}
		public static string RenderCheckboxFor<TViewModel>(this TViewModel viewModel, Func<TViewModel,bool> viewModelProperty, IDictionary<string,object> activeHtmlAttributes, IDictionary<string,object> inactiveHtmlAttributes)
		{
			var enabled = viewModelProperty.Invoke(viewModel);
			var src = (enabled) ? "/common/icons/True.png" : "/common/icons/False.png";
			var title = (enabled) ? "Active" : "Inactive";
			var alt = (enabled) ? "Active" : "Inactive";
			var htmlAttributes = (enabled) ? activeHtmlAttributes : inactiveHtmlAttributes;
			var img = new TagBuilder("img");
			img.Attributes.Add("src", src);
			img.Attributes.Add("title", title);
			img.Attributes.Add("alt", alt);
			if (htmlAttributes != null && htmlAttributes.Count> 0)
			{
				img.MergeAttributes(htmlAttributes, true);	
			}
			return img.ToString(TagRenderMode.SelfClosing);
		}
	}
}