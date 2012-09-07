using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public static string ReplaceString(this string input, string oldValue, string newValue, StringComparison comparison)
		{
			var sb = new StringBuilder();

			var previousIndex = 0;
			var index = input.IndexOf(oldValue, comparison);
			while (index != -1)
			{
				sb.Append(input.Substring(previousIndex, index - previousIndex));
				sb.Append(newValue);
				index += oldValue.Length;

				previousIndex = index;
				index = input.IndexOf(oldValue, index, comparison);
			}
			sb.Append(input.Substring(previousIndex));

			return sb.ToString();
		}
	}
}