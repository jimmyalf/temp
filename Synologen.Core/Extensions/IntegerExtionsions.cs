using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class IntegerExtionsions
	{
		//public static T ToEnum<T>(this int value) where T:struct 
		//{
		//    return (T) Enum.ToObject(typeof (T), value);
		//}

		public static char GetChar(this int value)
		{
			return GetChar(value, "ABCDEFGHIJKLMNOPQRSTUVXYZÅÄÖ");
		}

		public static char GetChar(this int value, string listOfAvailableChars)
		{
			var list = listOfAvailableChars.ToCharArray();
			return list[value % (list.Length - 1)];
		}

		public static void AsIndexFor(this int indexValue, Action<int> action)
		{
			action.Invoke(indexValue);
		}

		public static bool IsEven(this int value)
		{
			return (value % 2 == 0);
		}

		public static bool IsOdd(this int value)
		{
			return !value.IsEven();
		}
	}
}