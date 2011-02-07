using System;
using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Business.Extensions{
	public static class StringExtensions{

		public static string ToFormattedString<T>(this IEnumerable<T> list, string separator){
			if (list == null) return String.Empty;
			var returnString = String.Empty;
			foreach (var listItem in list){
				returnString += listItem + separator;
			}
			return returnString.TrimEnd(separator.ToCharArray());
		}
		public static string ToFormattedString<T>(this IEnumerable<T> list, Func<T,string> toStringMethod, string separator){
			if (list == null) return String.Empty;
			var returnString = String.Empty;
			foreach (var listItem in list){
				var addition = toStringMethod.Invoke(listItem);
				returnString += String.IsNullOrEmpty(addition) ? String.Empty : addition + separator;
			}
			return returnString.TrimEnd(separator.ToCharArray());
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> list){
			if(list==null) return true;
			return (list.Count() <= 0);
		}
		
	}
}