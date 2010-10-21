using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class DateTimeExtensions
	{
		public static bool IsSameDay(this DateTime value, DateTime compareValue)
		{
			return value.Year == compareValue.Year && value.Month == compareValue.Month && value.Day == compareValue.Day;
		}
	}
}