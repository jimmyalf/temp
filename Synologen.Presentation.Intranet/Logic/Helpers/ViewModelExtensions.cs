using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers {
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

        public static IEnumerable<FrameSupplierListItem> ToFrameSupplierList(this IEnumerable<FrameSupplier> list)
        {
            Func<FrameSupplier, FrameSupplierListItem> typeConverter = x => new FrameSupplierListItem
            {
                Id = x.Id,
                Name = x.Name,
            };
            return list.ConvertAll(typeConverter);
        }

        public static IEnumerable<DeviationDefectListItem> ToDeviationDefectList(this IEnumerable<DeviationDefect> list)
        {
            Func<DeviationDefect, DeviationDefectListItem> typeConverter = x => new DeviationDefectListItem
            {
                Id = x.Id,
                Name = x.Name,
            };
            return list.ConvertAll(typeConverter);
        }

        public static IEnumerable<DeviationSupplierListItem> ToDeviationSupplierList(this IEnumerable<DeviationSupplier> list)
        {
            Func<DeviationSupplier, DeviationSupplierListItem> typeConverter = x => new DeviationSupplierListItem
            {
                Id = x.Id,
                Name = x.Name,
            };
            return list.ConvertAll(typeConverter);
        }

        public static IEnumerable<DeviationCategoryListItem> ToDeviationCategoryList(this IEnumerable<DeviationCategory> list)
        {
            Func<DeviationCategory, DeviationCategoryListItem> typeConverter = x => new DeviationCategoryListItem
            {
                Id = x.Id,
                Name = x.Name,
            };
            return list.ConvertAll(typeConverter);
        }

		public static FrameOrder ToFrameOrder(this EditFrameFormEventArgs eventArgs, Frame frame, FrameGlassType glassType, Shop orderingShop)
		{
			var frameOrder = new FrameOrder {OrderingShop = orderingShop};
			return UpdateFrameOrder(frameOrder, frame, glassType, eventArgs);
		}

        public static FrameOrder FillFrameOrder(this EditFrameFormEventArgs eventArgs, Frame frame, FrameGlassType glassType, FrameOrder frameOrder)
		{
			return UpdateFrameOrder(frameOrder, frame, glassType, eventArgs);
		}

        private static FrameOrder UpdateFrameOrder(FrameOrder frameOrder, Frame frame, FrameGlassType glassType, EditFrameFormEventArgs eventArgs)
		{
			var enableAddition = (glassType != null) ? glassType.IncludeAdditionParametersInOrder : false;
			var enableHeight = (glassType != null) ? glassType.IncludeHeightParametersInOrder : false;
			frameOrder.Frame = frame;
			frameOrder.GlassType = glassType;
			frameOrder.Addition = eventArgs.ParseNullableEyeParameter(x => x.SelectedAddition, () => !enableAddition);
			frameOrder.Axis = ParseNullableEyeParameter(eventArgs.SelectedAxis);
			frameOrder.Created = DateTime.Now;
			frameOrder.Cylinder = eventArgs.ParseNullableEyeParameter(x => x.SelectedCylinder);
			frameOrder.Height = eventArgs.ParseNullableEyeParameter(x => x.SelectedHeight, () => !enableHeight);
			frameOrder.Reference = String.IsNullOrEmpty(eventArgs.Reference) ? null : eventArgs.Reference;
			frameOrder.PupillaryDistance = eventArgs.SelectedPupillaryDistance;
			frameOrder.Sent = null;
			frameOrder.Sphere = eventArgs.SelectedSphere;
			return frameOrder;
		}

		private static NullableEyeParameter ParseNullableEyeParameter(this EditFrameFormEventArgs eventArgs, Func<EditFrameFormEventArgs,EyeParameter> parameterProperty, Func<bool> disableCondition)
		{
			if(disableCondition()) return new NullableEyeParameter();
			var eyeParameter = parameterProperty(eventArgs);
			return new NullableEyeParameter 
			{
				Left = (eyeParameter.Left != int.MinValue) ? eyeParameter.Left : (decimal?)null,
				Right = (eyeParameter.Right != int.MinValue) ? eyeParameter.Right : (decimal?)null,
			};
		}

		private static NullableEyeParameter ParseNullableEyeParameter(this EditFrameFormEventArgs eventArgs, Func<EditFrameFormEventArgs,EyeParameter> parameterProperty)
		{
			return ParseNullableEyeParameter(eventArgs, parameterProperty, () => false);
		}

		private static NullableEyeParameter<int?> ParseNullableEyeParameter(EyeParameter<int> eyeParameter)
		{
			return new NullableEyeParameter<int?> 
			{
				Left = ( eyeParameter.Left != int.MinValue) ? eyeParameter.Left : (int?)null,
				Right = (eyeParameter.Right != int.MinValue) ? eyeParameter.Right : (int?)null,
			};
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
			var eyeParameter = new EyeParameter{Left = int.MinValue, Right = int.MinValue};
			if(selection != null)
			{
				eyeParameter = new EyeParameter
				{
					Left = (selection.Left.HasValue && listItems.Any(x => x.Value.Equals(selection.Left.Value.ToString("N2")))) ? selection.Left.Value : int.MinValue,
					Right = (selection.Right.HasValue && listItems.Any(x => x.Value.Equals(selection.Right.Value.ToString("N2")))) ? selection.Right.Value : int.MinValue
				};
			}
			return new EyeParameterIntervalListAndSelection
			{
				List = listItems.InsertDefaultValue(defaultValueText, int.MinValue),
                Selection = eyeParameter
			};
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
