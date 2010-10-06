using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class IntegerExtionsions
	{
		public static T ToEnum<T>(this int value) where T:struct 
		{
			return (T) Enum.ToObject(typeof (T), value);
		}
	}
}