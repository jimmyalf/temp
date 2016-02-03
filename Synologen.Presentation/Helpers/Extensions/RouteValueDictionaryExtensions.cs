using System.Linq;
using System.Web.Routing;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class RouteValueDictionaryExtensions
	{
		public static RouteValueDictionary TryRemoveRouteValue(this RouteValueDictionary dictionary, string key)
		{
			if(dictionary.ContainsKey(key)) dictionary.Remove(key);
			return dictionary;
		}

		public static RouteValueDictionary AddOrReplaceRouteValue(this RouteValueDictionary dictionary, string key, string value)
		{
			if(dictionary.ContainsKey(key)) dictionary[key] = value;
			else dictionary.Add(key, value);
			return dictionary;
		}

		public static RouteValueDictionary WhiteList(this RouteValueDictionary dictionary, params string[] whiteListKeys )
		{
			var returnValueDictionary = new RouteValueDictionary();
			var whiteList = whiteListKeys.ToArray();
			foreach (var item in dictionary)
			{
				if(whiteList.Contains(item.Key))
				{
					returnValueDictionary.Add(item.Key,item.Value);
				}
			}
			return returnValueDictionary;
		}

		public static RouteValueDictionary BlackList(this RouteValueDictionary dictionary, params string[] blackListKeys )
		{
			var returnValueDictionary = new RouteValueDictionary();
			var blackList = blackListKeys.ToArray();
			foreach (var item in dictionary)
			{
				if(!blackList.Contains(item.Key))
				{
					returnValueDictionary.Add(item.Key,item.Value);
				}
			}
			return returnValueDictionary;
		}
	}
}