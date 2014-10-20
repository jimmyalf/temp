using System;
using System.Collections.Generic;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Test.Core
{
	public static class Extensions
	{
		public static void ShouldBeOrderedAscendingBy<TModel>(this IEnumerable<TModel> list, Func<TModel,string> propertyToCompare)
		{
			list.ForEach().DoPairwise((item, nextItem) =>
			{
				var itemProperty = propertyToCompare(item);
				var nextProperty = propertyToCompare(nextItem);
				if(IsGreaterThan(itemProperty,nextProperty))
				{
					throw new SynologenTestException("List is not ordered ascending");
				}
			});
		}
		public static void ShouldBeOrderedDescendingBy<TModel>(this IEnumerable<TModel> list, Func<TModel,string> propertyToCompare)
		{
			list.ForEach().DoPairwise((item, nextItem) =>
			{
				var itemProperty = propertyToCompare(item);
				var nextProperty = propertyToCompare(nextItem);
				if(IsLessThan(itemProperty,nextProperty))
				{
					throw new SynologenTestException("List is not ordered decending");
				}
			});
		}
		public static void ShouldBeOrderedAscendingBy<TModel>(this IEnumerable<TModel> list, Func<TModel,int> propertyToCompare)
		{
			list.ForEach().DoPairwise((item, nextItem) =>
			{
				if(propertyToCompare(item) > propertyToCompare(nextItem))
				{
					throw new SynologenTestException("List is not ordered ascending");
				}
			});
		}
		public static void ShouldBeOrderedDescendingBy<TModel>(this IEnumerable<TModel> list, Func<TModel,int> propertyToCompare)
		{
			list.ForEach().DoPairwise((item, nextItem) =>
			{
				var itemProperty = propertyToCompare(item);
				var nextProperty = propertyToCompare(nextItem);
				if(itemProperty < nextProperty)
				{
					throw new SynologenTestException("List is not ordered decending");
				}
			});
		}

		public class SynologenTestException : Exception
		{
			public SynologenTestException(string message) : base(message) { }
		}
		private static bool IsLessThan(string thisItem, string otherItem)
		{
			return String.Compare(thisItem, otherItem, true) < 0;
		}
		private static bool IsGreaterThan(string thisItem, string otherItem)
		{
			return String.Compare(thisItem, otherItem, true) > 0;
		}
	}
}
