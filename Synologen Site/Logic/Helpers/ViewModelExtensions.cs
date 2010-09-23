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

		public static IEnumerable<FrameOrderListItemModel> ToFrameOrderListItems(this IEnumerable<FrameOrder> list)
		{
			Func<FrameOrder,FrameOrderListItemModel> typeConverter = frameOrder => new FrameOrderListItemModel {
				Id = frameOrder.Id,
                FrameName = frameOrder.Frame.Name,
                Sent = frameOrder.Sent.HasValue ? frameOrder.Sent.Value.ToString("yyyy-MM-dd HH:mm") : null
			};
			return list.ConvertAll(typeConverter);
		}

		public static FrameOrder ToFrameOrder(this EditFrameFormEventArgs eventArgs, Frame frame, FrameGlassType glassType, Shop orderingShop)
		{
			var frameOrder = new FrameOrder {Frame = frame, GlassType = glassType, OrderingShop = orderingShop};
			return UpdateFrameOrder(frameOrder, eventArgs);
		}

		public static FrameOrder FillFrameOrder(this EditFrameFormEventArgs eventArgs, FrameOrder frameOrder)
		{
			return UpdateFrameOrder(frameOrder, eventArgs);
		}

		private static FrameOrder UpdateFrameOrder(FrameOrder frameOrder, EditFrameFormEventArgs eventArgs)
		{
			frameOrder.Addition = new NullableEyeParameter
			{
				Left = (eventArgs.SelectedAddition.Left != int.MinValue) ? eventArgs.SelectedAddition.Left : (decimal?)null,
				Right = (eventArgs.SelectedAddition.Right != int.MinValue) ? eventArgs.SelectedAddition.Right : (decimal?)null,
			};
			frameOrder.Axis = new EyeParameter { Left = eventArgs.SelectedAxisLeft, Right = eventArgs.SelectedAxisRight };
			frameOrder.Created = DateTime.Now;
			frameOrder.Cylinder = eventArgs.SelectedCylinder;
			frameOrder.Height = new NullableEyeParameter {
				Left = (eventArgs.SelectedHeight.Left != int.MinValue) ? eventArgs.SelectedHeight.Left : (decimal?)null,
				Right = (eventArgs.SelectedHeight.Right != int.MinValue) ? eventArgs.SelectedHeight.Right : (decimal?)null,
			};
			frameOrder.Reference = String.IsNullOrEmpty(eventArgs.Reference) ? null : eventArgs.Reference;
			frameOrder.PupillaryDistance = eventArgs.SelectedPupillaryDistance;
			frameOrder.Sent = null;
			frameOrder.Sphere = eventArgs.SelectedSphere;
			return frameOrder;
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
				yield return new IntervalListItem {Name = value.ToString("N2"), Value = value.ToString("N2")};
			}
			yield break;

		}

		public static IEnumerable<IntervalListItem> InsertDefaultValue(this IEnumerable<IntervalListItem> list, string entityName, decimal NotSelectedValue)
		{
			var defaultValue = new IntervalListItem {Name = String.Format("-- Välj {0} --", entityName), Value = NotSelectedValue.ToString("N2")};
			return list.InsertFirst(defaultValue);
		}

		public static IEnumerable<IntervalListItem> InsertDefaultValue(string entityName, decimal NotSelectedValue)
		{
			var defaultValue = new IntervalListItem {Name = String.Format("-- Välj {0} --", entityName), Value = NotSelectedValue.ToString("N2")};
			return new List<IntervalListItem>().InsertFirst(defaultValue);
		}


		public static EyeParameterIntervalListAndSelection GetEyeParameter(this EditFrameFormEventArgs e, Func<EditFrameFormEventArgs,EyeParameter> selectedEyeParameters, IEnumerable<IntervalListItem> listItems, string defaultValueText)
		{
			
			var selection = selectedEyeParameters.Invoke(e);
			var returnValue = new EyeParameterIntervalListAndSelection
			{
				List = listItems.InsertDefaultValue(defaultValueText, int.MinValue),
				Selection = new EyeParameter
				{
					Left = listItems.Any(x => x.Value.Equals(selection.Left.ToString("N2"))) ? selection.Left : int.MinValue, 
					Right = listItems.Any(x => x.Value.Equals(selection.Right.ToString("N2"))) ? selection.Right : int.MinValue,
				}
			};
			return returnValue;
		}
		public static EyeParameterIntervalListAndSelection GetEyeParameter(this FrameOrder framOrder, Func<FrameOrder,EyeParameter> selectedEyeParameters, IEnumerable<IntervalListItem> listItems, string defaultValueText)
		{
			
			var selection = selectedEyeParameters.Invoke(framOrder);
			var returnValue = new EyeParameterIntervalListAndSelection
			{
				List = listItems.InsertDefaultValue(defaultValueText, int.MinValue),
				Selection = new EyeParameter
				{
					Left = listItems.Any(x => x.Value.Equals(selection.Left.ToString("N2"))) ? selection.Left : int.MinValue, 
					Right = listItems.Any(x => x.Value.Equals(selection.Right.ToString("N2"))) ? selection.Right : int.MinValue,
				}
			};
			return returnValue;
		}

		public static EyeParameterIntervalListAndSelection GetEyeParameter(this FrameOrder framOrder, Func<FrameOrder,NullableEyeParameter> selectedEyeParameters, IEnumerable<IntervalListItem> listItems, string defaultValueText)
		{
			
			var selection = selectedEyeParameters.Invoke(framOrder);
			var returnValue = new EyeParameterIntervalListAndSelection
			{
				List = listItems.InsertDefaultValue(defaultValueText, int.MinValue),
				Selection = new EyeParameter
				{
					Left = (selection.Left.HasValue && listItems.Any(x => x.Value.Equals(selection.Left.Value.ToString("N2")))) ? selection.Left.Value : int.MinValue,
					Right = (selection.Right.HasValue && listItems.Any(x => x.Value.Equals(selection.Right.Value.ToString("N2")))) ? selection.Right.Value : int.MinValue
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
