using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class ComparisonExtensions
	{
		public static bool HasOption<T>(this T flags, T option) where T : struct
		{
			var fl = Convert.ToInt32(flags);
			var opt = Convert.ToInt32(option);
			return (fl & opt) == opt;
		}
	}
}