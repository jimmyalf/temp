using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
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

		public static IEnumerable<FrameGlassTypeListItem> ToFrameGlassTypeViewList(this IEnumerable<FrameGlassType> list)
		{
			Func<FrameGlassType,FrameGlassTypeListItem> typeConverter = x => new FrameGlassTypeListItem {
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

		public static IEnumerable<IntervalListItem> GetList(this Interval interval)
		{
			foreach (var value in interval.ToList())
			{
				yield return new IntervalListItem {Name = value.ToString(), Value = value};
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


		public static EyeParameterIntervalListAndSelection GetEyeParameter(this FrameFormEventArgs e, Func<FrameFormEventArgs,EyeParameter> selectedEyeParameters, IEnumerable<IntervalListItem> listItems, string defaultValueText)
		{
			
			var selection = selectedEyeParameters.Invoke(e);
			var returnValue = new EyeParameterIntervalListAndSelection
			{
				List = listItems.InsertDefaultValue(defaultValueText, int.MinValue),
				Selection = new EyeParameter
				{
					Left = listItems.Any(x => x.Value.Equals(selection.Left)) ? selection.Left : int.MinValue, 
					Right = listItems.Any(x => x.Value.Equals(selection.Right)) ? selection.Right : int.MinValue,
				}
			};
			return returnValue;
		}

		public static EyeParameterIntervalListAndSelection CreateDefaultEyeParameter(this IEnumerable<IntervalListItem> listItems, string defaultValueText)
		{
			var returnValue = new EyeParameterIntervalListAndSelection
			{
				List = listItems.InsertDefaultValue(defaultValueText, int.MinValue), 
				Selection = new EyeParameter {Left = int.MinValue, Right = int.MinValue}
			};
			return returnValue;
		}
	}
}
