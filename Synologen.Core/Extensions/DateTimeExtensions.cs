using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class DateTimeExtensions
	{
		public static bool IsSameDay(this DateTime value, DateTime compareValue)
		{
			return value.Year == compareValue.Year && value.Month == compareValue.Month && value.Day == compareValue.Day;
		}

		public static DateTime SubtractDays(this DateTime value, int numberOfDays)
		{
			return value.AddDays(numberOfDays*-1);
		}

		public static DateTime SubtractMonths(this DateTime value, int numberOfMonths)
		{
			return value.AddMonths(numberOfMonths*-1);
		}
	}
}