using System;
using System.Linq;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers
{
	public static class FactoryHelpers
	{
		public static string Reverse(this string input)
		{
			return new string(input.ToCharArray().Reverse().ToArray());
		}

		public static T Next<T>(this T src) where T : struct {
			if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is Not of enum type",typeof(T).FullName));

			var Arr = (T[])Enum.GetValues(src.GetType());
			var j = Array.IndexOf(Arr,src)+1;
			return (Arr.Length==j)?Arr[0]:Arr[j];            
		}
	}
}