using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.AssertionHelpers
{
	public class WpcAssertionHelper : AssertionHelper
	{
		public void ExpectEqual<T>(T actual, T expected, IEqualityComparer<T> comparer)
        {
			Assert.That(comparer.Equals(actual, expected), Is.True, null, null);
        }
		public void ExpectEqual<T1,T2>(T1 actual, T2 expected, Func<T1,T2,bool> equalFunction)
        {
			Assert.That(equalFunction(actual, expected), Is.True, null, null);
        }
	}
}