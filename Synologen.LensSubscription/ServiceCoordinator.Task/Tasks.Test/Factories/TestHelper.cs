using System;
using System.Collections.Generic;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
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
	}
}