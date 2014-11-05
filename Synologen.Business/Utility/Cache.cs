using System;
using System.Data;
using System.Web;

namespace Spinit.Wpc.Synologen.Business.Utility {
	public class Cache {
		public static string GetCacheValue(string cacheName, string defaultValue) {
			try {
				return HttpContext.Current.Cache[cacheName].ToString();
			}
			catch { return defaultValue; }
		}

		public static int GetCacheValue(string cacheName, int defaultValue) {
			try {
				int returnValue;
				return Int32.TryParse(HttpContext.Current.Cache[cacheName].ToString(), out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		public static bool GetCacheValue(string cacheName, bool defaultValue) {
			try { return (bool)(HttpContext.Current.Cache[cacheName]); }
			catch { return defaultValue; }
		}

		public static DataSet GetCacheValue(string cacheName, DataSet defaultValue) {
			try { return (DataSet)(HttpContext.Current.Cache[cacheName]); }
			catch { return defaultValue; }
		}

		public static void SetCacheValue(string cacheName, object value, int cacheExpirationTimoutInMinutes) {
			DateTime absoluteExpiration = DateTime.MaxValue;

			//Setting DateTime.Now below enables turning of cache at 0 timeout instead of infinite caching
			if(cacheExpirationTimoutInMinutes == 0) {
				absoluteExpiration = DateTime.Now;
			}
			HttpContext.Current.Cache.Insert(cacheName, value, null, absoluteExpiration, new TimeSpan(0, cacheExpirationTimoutInMinutes, 0));
		}

		public static object GetCacheValue(string cacheName) {
			try {
				object list = HttpContext.Current.Cache[cacheName];
				return list ?? new object();
			}
			catch { return new object(); }
		}
	}
}