using System;
using NUnit.Framework.Constraints;

namespace Spinit.Wpc.Synologen.Autogiro.Test.Helpers
{
	public class DateTimeTestConstraint : Constraint
	{
		private readonly DateTime _expected;
		private readonly DateTimeTolerance _tolerance;

		public DateTimeTestConstraint(DateTime expected, DateTimeTolerance tolerance)
		{
			_expected = expected;
			_tolerance = tolerance;
		}

		public override bool Matches(object actualValue)
		{
			actual = actualValue;
			var actualDateTime = actual as DateTime?;
			if(!actualDateTime.HasValue) return false;
			switch (_tolerance)
			{
				case DateTimeTolerance.SameYear:
					return actualDateTime.Value.Year.Equals(_expected.Year);
				case DateTimeTolerance.SameYearMonth:
					return ( actualDateTime.Value.Year.Equals(_expected.Year) 
					         && actualDateTime.Value.Month.Equals(_expected.Month) );
				case DateTimeTolerance.SameYearMonthDate:
					return ( actualDateTime.Value.Year.Equals(_expected.Year) 
					         && actualDateTime.Value.Month.Equals(_expected.Month) 
					         && actualDateTime.Value.Day.Equals(_expected.Day));
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		public override void WriteDescriptionTo(MessageWriter writer) { 
			writer.WriteMessageLine("The expected date {0} does not match given date {1} within tolerance", _expected.ToString(), actual.ToString());
		}
	}
}