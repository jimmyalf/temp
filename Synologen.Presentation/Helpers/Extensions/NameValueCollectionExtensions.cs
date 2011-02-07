using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class NameValueCollectionExtensions
	{
		public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection nameValueCollection)
		{
			var dictionary = new RouteValueDictionary();
			foreach (var collectionKey in nameValueCollection.AllKeys)
			{
				dictionary.Add(collectionKey, nameValueCollection[collectionKey]);
			}
			return dictionary;
		}

		public static NameValueCollection AddReplaceItem(this NameValueCollection collection, string key, string value)
		{
			var internalNameValueCollection = new NameValueCollection(collection);
			if(String.IsNullOrEmpty(internalNameValueCollection[key]))
			{
				internalNameValueCollection.Add(key, HttpUtility.HtmlEncode(value));
			}
			else
			{
				internalNameValueCollection[key] = HttpUtility.HtmlEncode(value);
			}
			return internalNameValueCollection;
		}

		public static string ToQueryString(this NameValueCollection collection)
		{
			var builder = new StringBuilder();
			foreach (string key in collection.Keys)
            {
				if(builder.Length>0)
				{
					builder.AppendFormat("&amp;{0}={1}", key, HttpUtility.HtmlEncode(collection[key]));	
				}
				else
				{
					builder.AppendFormat("{0}={1}", key, HttpUtility.HtmlEncode(collection[key]));	
				}
            	
            }
			return builder.ToString();
		}
	}
}