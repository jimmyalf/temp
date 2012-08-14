using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Extensions
{
	public static class StringExtensions
	{
		 public static string RegexReplace(this string input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
		 {
		 	return Regex.Replace(input, pattern, replacement, options);
		 }
		 public static string RegexRemove(this string input, string pattern, RegexOptions options = RegexOptions.None)
		 {
		 	return Regex.Replace(input, pattern, String.Empty, options);
		 }

		public static string ConvertToString(this IEnumerable<char> charArray)
		{
			return new string(charArray.ToArray());
		}
	}
}