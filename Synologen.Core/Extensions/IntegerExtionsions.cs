using System;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class IntegerExtionsions
	{
		public static char GetChar(this int value)
		{
			return GetChar(value, "ABCDEFGHIJKLMNOPQRSTUVXYZ���");
		}

		public static char GetChar(this int value, string listOfAvailableChars)
		{
			var list = listOfAvailableChars.ToCharArray();
			var index = value % (list.Length);
			return list[index];
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