using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static  class DoubleExtensions
	{
		public static decimal ToDecimal(this double value)
		{
			return Convert.ToDecimal(value);
		}
	}
}