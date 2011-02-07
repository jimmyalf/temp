using System;
using System.Collections.Generic;

namespace Synologen.ServiceCoordinator.Test.Factories
{
	public static class TestHelper
	{
		public static IEnumerable<TModel> GenerateSequence<TModel>(Func<int,TModel> generationFunction,int numberOfItems)
		{
			for (var i = 1; i <= numberOfItems; i++)
			{
				yield return generationFunction(i);
			}
			yield break;
		}

		public static T Next<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is Not of enum type", typeof(T).FullName));

			var Arr = (T[])Enum.GetValues(src.GetType());
			var j = Array.IndexOf(Arr, src) + 1;
			return (Arr.Length == j) ? Arr[0] : Arr[j];
		}
	}
}