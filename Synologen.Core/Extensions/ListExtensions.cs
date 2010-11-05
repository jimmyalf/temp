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

		public static void For<TType>(this IEnumerable<TType> list, Action<int,TType> enumerableAction)
		{
			var index = 0;
			foreach (var item in list)
			{
				enumerableAction.Invoke(index, item);
				index++;
			}
		}

		public static void ForElementAtIndex<TType>(this IEnumerable<TType> list, int index, Action<TType> action)
		{
			action.Invoke(list.ElementAt(index));
		}


		public static IEnumerable<TType> Except<TType>(this IEnumerable<TType> list, IgnoreType type)
		{
			switch (type)
			{
				case IgnoreType.First:
					return list.Skip(1);
				case IgnoreType.Last:
					return list.Take(list.Count() - 1);
				case IgnoreType.FirstAndLast:
					return list.Except(IgnoreType.First).Except(IgnoreType.Last);
				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

		public enum IgnoreType
		{
			First,
			Last,
			FirstAndLast
		}

	}


}