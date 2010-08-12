using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Models;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers {
	public static class ViewModelExtensions {

		public static IEnumerable<FrameListItem> ToFrameViewList(this IEnumerable<Frame> list)
		{
			Func<Frame,FrameListItem> typeConverter = x => new FrameListItem {
				Id = x.Id,
				Name = x.Name,
			};
			return list.ConvertAll(typeConverter);
		}

		public static IEnumerable<TModel> InsertFirst<TModel>(this IEnumerable<TModel> list, TModel item)
		{
			var returnList =  list.ToList();
			returnList.Insert(0, item);
			return returnList;
		}

		public static IEnumerable<IntervalListItem> GetIntervalListFor(this Frame frame, Expression<Func<Frame, Interval>> expression)
		{
			if (frame == null) yield break;
			var interval = expression.Compile().Invoke(frame);
			if (interval.Increment <= 0) yield break;
			var currentValue = interval.Min;
			while (currentValue <= interval.Max)
			{
				yield return new IntervalListItem {Name = currentValue.ToString(), Value = currentValue};
				currentValue += interval.Increment;
			}
			yield break;
		}

		public static IEnumerable<IntervalListItem> InsertDefaultValue(this IEnumerable<IntervalListItem> list, string entityName, decimal NotSelectedValue)
		{
			var defaultValue = new IntervalListItem {Name = String.Format("-- Välj {0} --", entityName), Value = NotSelectedValue};
			return list.InsertFirst(defaultValue);
		}

		public static IEnumerable<IntervalListItem> InsertDefaultValue(string entityName, decimal NotSelectedValue)
		{
			var defaultValue = new IntervalListItem {Name = String.Format("-- Välj {0} --", entityName), Value = NotSelectedValue};
			return new List<IntervalListItem>().InsertFirst(defaultValue);
		}
	}
}
