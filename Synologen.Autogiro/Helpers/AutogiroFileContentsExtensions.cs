using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro.Helpers
{
	public static class AutogiroFileContentsExtensions
	{
		public static string[] SplitIntoRows(this string content)
		{
			return content.Trim(' ','\r','\n')
				.Split(new[]{ '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
		}

		public static DateTime ParseDate(this string dateAsString)
		{
			if(dateAsString.Length != 8)
			{
				throw new ArgumentException("String to be parsed into date is not 8 characters long as expected", "dateAsString");
			}
			var year = dateAsString.ReadFromIndex(0).ToIndex(3).ToInt();
			var month = dateAsString.ReadFromIndex(4).ToIndex(5).ToInt();
			var day = dateAsString.ReadFromIndex(6).ToIndex(7).ToInt();
			return new DateTime(year, month, day);
		}

		public static EnumType ParseEnum<EnumType>(this string enumAsString) 
			where EnumType : struct 
		{
			return enumAsString.ToInt().ToEnum<EnumType>();
		}

		public static T? ParseNullable<T>(this string value, Func<string,T> converter)
			where T:struct 
		{
			if(String.IsNullOrEmpty(value.Replace(" ",""))) return null;
			return converter(value);
		}

		public static decimal ParseAmount(this string value)
		{
			if(value.Length < 3)
			{
				throw new ArgumentException("Cannot parse into decimal, string is too short", "value");
			}
			var commaIndex = value.Length - 2;
			var valueWithInsertedCommaSeparator = value.Insert(commaIndex, ",").ToDecimal();
			return valueWithInsertedCommaSeparator;
		}

		public static PaymentResult ParsePaymentResult(this string value)
		{
			if(value.Length != 1)
			{
				throw new ArgumentException("Cannot parse payment result, string length is not one", "value");
			}
			return value == " " ? PaymentResult.Approved : value.ParseEnum<PaymentResult>();
		}
	}
}