using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Test.App
{
	public static class AssertionExtensions
	{
		public static void AssertContains<T>(this IEnumerable<T> source, Expression<Func<T, bool>> predicate)
		{
			if(source.Any(predicate.Compile())) return;
			throw GetError("source did not contain any items matching {Predicate}", predicate);
		}

		public static void AssertDoesNotContain<T>(this IEnumerable<T> source, Expression<Func<T,bool>> predicate)
		{
			if (source.Any(predicate.Compile())) throw GetError("source did contain items matching {Predicate}", predicate);
			return;
		}

		public static void AssertIsEmpty<T>(this IEnumerable<T> source)
		{
			if (source == null || source.Count() == 0) return;
			var items = source.Select(x => x.ToString()).Aggregate((a, b) =>  a + ", " + b);
			throw GetError("source is not empty, it contains the following items:\r\n" + items);
		}

		private static AssertionException GetError<T,T2>(string message, Expression<Func<T,T2>> predicate)
		{
			return GetError(message.Replace("{Predicate}", predicate.ToString()));
		}

		private static AssertionException GetError(string message)
		{
			return new AssertionException(message);
		}
	}
}