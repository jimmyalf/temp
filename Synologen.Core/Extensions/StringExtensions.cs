using System;
using System.Linq;
using System.Text.RegularExpressions;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class StringExtensions
	{
		public static string AppendUrl(this string value, string urlToAppend)
		{
			return string.Concat(value.TrimEnd('/'), "/", urlToAppend.TrimStart('/'));
		}

		public static string FormatPersonalIdNumber(this string value)
		{
			if(value.Length != 12)
			{
				throw new ArgumentException("Invalid length (personal Id number length needs to be 12 digits long)", "value");
			}
			return value.Insert(8, "-");
		}

		public static byte[] ToByteArray(this string hexString)
		{
			if(String.IsNullOrEmpty(hexString)) return new byte[0];
			return Enumerable.Range(0, hexString.Length)
				.Where(x => 0 == x % 2)
				.Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
				.ToArray();
		}

		public static string RegexReplace(this string input, string pattern, string replacement)
		{
			if(input == null) return null;
			if(input == string.Empty) return string.Empty;
			return Regex.Replace(input, pattern, replacement);
		}

		public static string RegexRemove(this string input, string pattern)
		{
			return input.RegexReplace(pattern, String.Empty);
		}

		public static bool ContainsAny(this string value, params string[] comparisonValues)
		{
			return comparisonValues.Any(value.Contains);
		}

		public static int? ToNullableInt(this string value)
		{
			if (String.IsNullOrEmpty(value)) return null;
			return value.ToInt();
		}

		public static decimal? ToNullableDecimal(this string value)
		{
			if (String.IsNullOrEmpty(value)) return null;
			return value.ToDecimal();
		}
	}
}