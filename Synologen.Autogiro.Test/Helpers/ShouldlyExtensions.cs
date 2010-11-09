using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace Spinit.Wpc.Synologen.Autogiro.Test.Helpers
{
	[ShouldlyMethods]
	public static class ShouldlyExtensions
	{
		public static void ShouldBe(this DateTime actual, DateTime expected, DateTimeTolerance tolerance) 
		{
			actual.AssertAwesomely(new DateTimeTestConstraint(expected, tolerance), actual, expected);
		}
		public static void ShouldBeEmpty<TModel>(this IEnumerable<TModel> list) 
		{
			list.Count().ShouldBe(0);
		}
	}

	public enum DateTimeTolerance
	{
		SameYear,
		SameYearMonth,
		SameYearMonthDate
	}
}