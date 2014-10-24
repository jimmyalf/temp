using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Invoicing.Test.App
{
	public static class AssertionExtensions
	{
		public static void AssertContains<T>(this IEnumerable<T> source, Expression<Func<T, bool>> predicate)
		{
			if (source.Any(predicate.Compile()))
			{
			    return;
			}

			throw GetError("source did not contain any items matching {Predicate}", predicate);
		}

		public static void AssertDoesNotContain<T>(this IEnumerable<T> source, Expression<Func<T,bool>> predicate)
		{
			if (source.Any(predicate.Compile()))
			{
			    throw GetError("source did contain items matching {Predicate}", predicate);
			}
		}

		public static void AssertIsEmpty<T>(this IEnumerable<T> source)
		{
			if (source == null || !source.Any())
			{
			    return;
			}

			var items = source.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b);
			throw GetError("source is not empty, it contains the following items:\r\n" + items);
		}

		public static void AssertIsNotEmpty<T>(this IEnumerable<T> source)
		{
		    if (source == null || !source.Any())
		    {
		        throw GetError("source does not contain any items");
		    }
		}

	    private static AssertionFailedException GetError<T,T2>(string message, Expression<Func<T,T2>> predicate)
		{
			return GetError(message.Replace("{Predicate}", predicate.ToString()));
		}

		private static AssertionFailedException GetError(string message)
		{
			return new AssertionFailedException(message);
		}
	}
}