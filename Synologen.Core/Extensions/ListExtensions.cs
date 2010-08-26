using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class ListExtensions
	{
		public static bool HasKey(this NameObjectCollectionBase collection, string key)
		{
			if(collection == null  || collection.Keys == null) return false;
			foreach (var keyItem in collection.Keys)
			{
				if(keyItem.Equals(key)) return true;
			}
			return false;
		}

		public static ISortedPagedList<TModel> ToSortedPagedList<TModel>(this IEnumerable<TModel> list)
		{
			return list as ISortedPagedList<TModel>;
		}

		public static IEnumerable<TToModel> ConvertAll<TFromModel, TToModel>(this IEnumerable<TFromModel> input, Func<TFromModel,TToModel> converterFunction)
		{
			return input.ToList().ConvertAll(new Converter<TFromModel, TToModel>(converterFunction));
		}

	}
}