using System;
using System.Collections.Generic;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
    public class TestHelper
    {
        public static IEnumerable<TModel> GenerateSequence<TModel>(Func<TModel> generationFunction, int numberOfItems)
        {
            for (var i = 1; i <= numberOfItems; i++)
            {
                yield return generationFunction();
            }
            yield break;
        }
    }
}
