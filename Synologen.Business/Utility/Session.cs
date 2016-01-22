using System;
using System.Collections.Generic;
using System.Web;

namespace Spinit.Wpc.Synologen.Business.Utility {
	public class Session {

        public static string GetSessionValue(string sessionName, string defaultValue) {
			try {
				return HttpContext.Current.Session[sessionName].ToString();
			}
			catch { return defaultValue; }
		}

		public static int GetSessionValue(string sessionName, int defaultValue) {
			try {
				int returnValue;
				return Int32.TryParse(HttpContext.Current.Session[sessionName].ToString(), out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		//public static bool GetSessionValue(string sessionName, bool defaultValue) {
		//    try { return (bool)(HttpContext.Current.Session[sessionName]); }
		//    catch { return defaultValue; }
		//}

		public static TModel GetSessionValue<TModel>(string sessionName, TModel defaultValue) where TModel:struct
		{
			try { return (TModel)(HttpContext.Current.Session[sessionName]); }
			catch { return defaultValue; }
		}

		//public static DateTime GetSessionValue(string sessionName, DateTime defaultValue) {
		//    try { return (DateTime)(HttpContext.Current.Session[sessionName]); }
		//    catch { return defaultValue; }
		//}

		public static void SetSessionValue(string sessionName, object value) {
			HttpContext.Current.Session[sessionName] = value;
		}

		public static object GetSessionValue(string sessionName) {
			try {
				object list = HttpContext.Current.Session[sessionName];
				return list ?? new object();
			}
			catch { return new object(); }
		}

		public static List<int> GetSessionIntList(string sessionName) {
			try {
				List<int> list = (List<int>)HttpContext.Current.Session[sessionName];
				return list ?? new List<int>();
			}
			catch { return new List<int>(); }
		}
	}
}