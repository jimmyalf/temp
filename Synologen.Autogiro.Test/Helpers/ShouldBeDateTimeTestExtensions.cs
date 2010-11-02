using System;
using Shouldly;

namespace Spinit.Wpc.Synologen.Autogiro.Test.Helpers
{
	[ShouldlyMethods]
	public static class ShouldBeDateTimeTestExtensions
	{
		public static void ShouldBe(this DateTime actual, DateTime expected, DateTimeTolerance tolerance) 
		{
			actual.AssertAwesomely(new DateTimeTestConstraint(expected, tolerance), actual, expected);
		}
	}

	public enum DateTimeTolerance
	{
		SameYear,
		SameYearMonth,
		SameYearMonthDate
	}
}