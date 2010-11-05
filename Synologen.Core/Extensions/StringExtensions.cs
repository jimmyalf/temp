using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class StringExtensions
	{
		public static decimal ToDecimalOrDefault(this string textValue, decimal defaultValue)
		{
			decimal output;
			return decimal.TryParse(textValue, out output) ? output : defaultValue;
		}

		public static decimal ToDecimalOrDefault(this string textValue)
		{
			decimal output;
			return decimal.TryParse(textValue, out output) ? output : default(decimal);
		}

		public static decimal ToDecimal(this string textValue)
		{
			return decimal.Parse(textValue);
		}

		public static int ToIntOrDefault(this string textValue, int defaultValue)
		{
			int output;
			return int.TryParse(textValue, out output) ? output : defaultValue;	
		}

		public static int ToIntOrDefault(this string textValue)
		{
			int output;
			return int.TryParse(textValue, out output) ? output : default(int);
		}

		public static int ToInt(this string textValue)
		{
			return int.Parse(textValue);
		}

		public static string Reverse(this string value)
		{
			var charArray = value.ToCharArray().ToEnumerable().Reverse().ToArray();
			return new string(charArray);
		}

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
	}
}