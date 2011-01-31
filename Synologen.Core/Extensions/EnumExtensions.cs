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

		public static string ToNumberString(this Enum value)
		{
			return value.ToInteger().ToString();
		}

		public static T Next<T>(this T src) where T : struct 
		{
			if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is Not of enum type",typeof(T).FullName));

			var Arr = (T[])Enum.GetValues(src.GetType());
			var j = Array.IndexOf(Arr,src)+1;
			return (Arr.Length==j)?Arr[0]:Arr[j];            
		}

		public static T SkipValues<T>(this T src, int numberOfSkips) where T : struct
		{
			var allEnumValues = (T[])Enum.GetValues(src.GetType());
			var enumTopIndex = allEnumValues.Count();
			var skipIndex = numberOfSkips % enumTopIndex;
			return allEnumValues[skipIndex];
		}
	}
}