using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class WpcGridRendererExtensions
	{
		public static IDictionary<string,object> SetClass(this IDictionary<string,object> attributes, string className)
		{
			if(attributes.ContainsKey("class") && attributes["class"] != null)
			{
				className = string.Join(" ", new[] { attributes["class"].ToString(), className });
			}
			attributes["class"] = className;
			return attributes;
		}
		public static Dictionary<string,object> SetClass(this Dictionary<string,object> attributes, string className)
		{
			if(attributes.ContainsKey("class") && attributes["class"] != null)
			{
				className = string.Join(" ", new[] { attributes["class"].ToString(), className });
			}
			attributes["class"] = className;
			return attributes;
		}
	}
}