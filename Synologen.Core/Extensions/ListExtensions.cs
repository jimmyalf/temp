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
			return list.ToArray().Concat(listToAppend);
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

		public static void ForBoth<TType1,TType2>(this IEnumerable<TType1> list1, IList<TType2> list2,  Action<TType1, TType2> enumerableAction)
		{
			if (list1.Count() != list2.Count()) throw new ArgumentOutOfRangeException("list2", "Lists have different length");
			var index = 0;
			foreach (var item in list1)
			{
				enumerableAction.Invoke(item, list2.ElementAt(index));
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

		public static IEnumerable<TModel> GenerateRange<TModel>(this Func<int,TModel> converter, int startIndex, int count )
		{
			var range = Enumerable.Range(startIndex, count);
			foreach (var index in range)
			{
				yield return converter(index);
			}
			yield break;
		}

		public static IEnumerable<TModel> GenerateRange<TModel>(this Func<TModel> converter, int count )
		{
			for (var i = 0; i < count; i++)
			{
				yield return converter();
			}
			yield break;
		}

		public static string ToHexString(this byte[] bytes)
		{
			if(bytes == null || bytes.Count() == 0) return String.Empty;
			return bytes
				.SelectMany(byteItem => byteItem.ToString("X2"))
				.AsString();
		}

		public static string AsString(this IEnumerable<char> characterList)
		{
			if(characterList == null) return null;
			return characterList.Count() == 0 
				? String.Empty 
				: new string(characterList.ToArray());
		}

	}


}