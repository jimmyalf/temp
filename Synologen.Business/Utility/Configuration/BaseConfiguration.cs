using System;
using System.Configuration;
using System.Globalization;

namespace Spinit.Wpc.Synologen.Business.Utility.Configuration 
{
	public class BaseConfiguration 
	{
		public static string GetSafeValue(string key, string defaultValue) 
		{
			try {
				var value = ConfigurationManager.AppSettings[key];
				return String.IsNullOrEmpty(value) ? defaultValue : value;
			}
			catch { return defaultValue; }
		}

		public static int GetSafeValue(string key, int defaultValue) 
		{
			try {
				var value = ConfigurationManager.AppSettings[key];
				int returnValue;
				return Int32.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}
		public static int? GetSafeValue(string key, int? defaultValue) 
		{
			try {
				var value = ConfigurationManager.AppSettings[key];
				int returnValue;
				return Int32.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		public static bool GetSafeValue(string key, bool defaultValue) 
		{
			try {
				var value = ConfigurationManager.AppSettings[key];
				bool returnValue;
				return bool.TryParse(value, out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		public static float GetSafeValue(string key, float defaultValue) 
		{
			try {
				var value = ConfigurationManager.AppSettings[key];
				return Parse(value, defaultValue);

			}
			catch { return defaultValue; }
		}
		public static decimal GetSafeValue(string key,decimal defaultValue)
		{
			try {
				var value = ConfigurationManager.AppSettings[key];
				return Parse(value, defaultValue);
			}
			catch { return defaultValue; }
		}

		#region Helper Methods
		private static decimal Parse(string stringValue, decimal defaultValue)
		{
			var commaReplacedValue = stringValue.Replace(',', '.');
			decimal outputValue;
			var result = decimal.TryParse(commaReplacedValue, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out outputValue);
			return (result) ? outputValue : defaultValue;
		}
		private static float Parse(string stringValue, float defaultValue)
		{
			var commaReplacedValue = stringValue.Replace(',', '.');
			float outputValue;
			var result = float.TryParse(commaReplacedValue, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out outputValue);
			return (result) ? outputValue : defaultValue;
		}
		#endregion
	}
}