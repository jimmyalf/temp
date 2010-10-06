using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class EnumExtensions
	{
        public static string GetEnumDisplayName(this Enum value) 
		{
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attribs = fieldInfo.GetCustomAttributes(typeof(EnumDisplayNameAttribute), false) as EnumDisplayNameAttribute[];
			if(attribs == null) return null;
            return attribs.Length > 0 ? attribs[0].DisplayName : null;
        }

		public static IEnumerable<TEnumType> Enumerate<TEnumType>()
		{
			foreach (TEnumType item in  Enum.GetValues(typeof(TEnumType)))
			{
				yield return item;
			}
			yield break;
		}

		public static TType AppendFlags<TType>(this IEnumerable<TType> enumFlags) where TType :struct 
		{
			return enumFlags.Aggregate(AppendFlags);
		}
		public static TType AppendFlags<TType>(this TType flagOne, TType flagTwo) where TType :struct 
		{
			return (TType)(object)(((int)(object)flagOne | (int)(object)flagTwo));
		}

		public static int ToInteger(this Enum value)
		{
			return Convert.ToInt32(value);
		}
	}
}