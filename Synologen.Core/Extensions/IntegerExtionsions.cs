using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class IntegerExtionsions
	{
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

		public static bool IsDivisibleBy(this int value, int divisibleByValue)
		{
			return (value % divisibleByValue == 0);
		}
	}
}