using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

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

		public static IEnumerable<TToModel> ConvertAll<TFromModel, TToModel>(this IEnumerable<TFromModel> input, Func<TFromModel,TToModel> converterFunction)
		{
			return input.ToList().ConvertAll(new Converter<TFromModel, TToModel>(converterFunction));
		}

		public static IExtendedEnumerable<TToModel> ConvertAll<TFromModel, TToModel>(this IExtendedEnumerable<TFromModel> input, Func<TFromModel,TToModel> converterFunction) where TToModel : class
		{
			return new ExtendedEnumerable<TToModel>(
			    input.ToList().ConvertAll(new Converter<TFromModel, TToModel>(converterFunction)),
			    input.TotalCount, 
			    input.Page, 
			    input.PageSize, 
			    input.SortedBy, 
			    input.SortedAscending
			);
		}

		public static IEnumerable<TType> Ignore<TType>(this IEnumerable<TType> list, params TType[] ignore)
		{
			foreach (var item in  list)
			{
				if(ignore.Contains(item)) continue;
				yield return item;
			}
			yield break;
		}

		public static IEnumerable<TType> ToEnumerable<TType>(this TType[] list)
		{
			foreach (var item in  list)
			{
				yield return item;
			}
			yield break;
		}

		public static IEnumerable<TType> Append<TType>(this IEnumerable<TType> list, IEnumerable<TType> listToAppend )
		{
			return listToAppend.ToArray().Concat(listToAppend);
		}

		public static IEnumerable<TType> ForEach<TType>(this IEnumerable<TType> list, IEnumerable<TType> listToAppend )
		{
			return listToAppend.ToArray().Concat(listToAppend);
		}
	}
}