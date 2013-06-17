using System;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Core.Utility
{
	public static class Ensure
	{
		public static void NotNull<T>(Expression<Func<T>> property, T value)
		{
			if (value == null) throw new ArgumentNullException(GetParameterName(property), "Parameter cannot be null.");
		}

		public static void NotNullOrEmpty(Expression<Func<string>> property, string value)
		{
			NotNull(property, value);
			if (value.Length == 0) throw new ArgumentException("Parameter cannot be empty.", GetParameterName(property));
		}

		private static string GetParameterName(Expression reference)
		{
			var lambda = reference as LambdaExpression;
			if (lambda == null) return null;
			var member = lambda.Body as MemberExpression;
			return member == null ? null : member.Member.Name;
		}
	}
}