using System;
using System.Configuration;

namespace Spinit.Wpc.Synologen.ServiceLibrary.ConfigurationSettings {
	public class BaseConfiguration {
		public static string GetSafeValue(string key, string defaultValue) {
			try {
				var value = ConfigurationManager.AppSettings[key];
				return String.IsNullOrEmpty(value) ? defaultValue : value;
			}
			catch { return defaultValue; }
		}

		public static int GetSafeValue(string key, int defaultValue) {
			try {
				var value = ConfigurationManager.AppSettings[key];
				int returnValue;
				return Int32.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}
		public static int? GetSafeValue(string key, int? defaultValue) {
			try {
				var value = ConfigurationManager.AppSettings[key];
				int returnValue;
				return Int32.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		public static bool GetSafeValue(string key, bool defaultValue) {
			try {
				var value = ConfigurationManager.AppSettings[key];
				bool returnValue;
				return bool.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		public static float GetSafeValue(string key, float defaultValue) {
			try {
				var value = ConfigurationManager.AppSettings[key];
				float returnValue;
				return float.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}
		public static decimal GetSafeValue(string key,decimal defaultValue) {
			try {
				var value = ConfigurationManager.AppSettings[key];
				decimal returnValue;
				return decimal.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}
	}
}