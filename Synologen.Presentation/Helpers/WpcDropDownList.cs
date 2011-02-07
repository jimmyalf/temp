using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public static class WpcDropDownList
	{
		public static MvcHtmlString WpcDropDownListFor<TViewModel, TDropDownListModel, TType>(this HtmlHelper<TViewModel> helper, Expression<Func<TViewModel,TType>> viewModelProperty , Func<TViewModel,IEnumerable<TDropDownListModel>> listProperty, Expression<Func<TDropDownListModel,TType>> valueProperty, Expression<Func<TDropDownListModel,object>> displayProperty, string optionLabel, TType selectedValue) 
			where TDropDownListModel : class 
			where TType : struct
		{
			var model = helper.ViewData.Model;
			var list = listProperty.Invoke(model);
			var selectList = list.ToSelectList(valueProperty, displayProperty, selectedValue);
			return helper.DropDownListFor(viewModelProperty, selectList, optionLabel);
		}

		public static MvcHtmlString WpcDropDownListFor<TViewModel,TDropDownListModel, TType>(this HtmlHelper<TViewModel> helper, Expression<Func<TViewModel,TType>> viewModelProperty , Func<TViewModel,IEnumerable<TDropDownListModel>> listProperty, Expression<Func<TDropDownListModel,TType>> valueProperty, Expression<Func<TDropDownListModel,object>> displayProperty, string optionLabel) 
			where TDropDownListModel:class 
			where TType : struct 
		{
			var model = helper.ViewData.Model;
			var selectedValue = viewModelProperty.Compile().Invoke(model);
			return WpcDropDownListFor(helper, viewModelProperty, listProperty, valueProperty, displayProperty, optionLabel, selectedValue);
		}

		public static MvcHtmlString WpcDropDownListFor<TViewModel,TDropDownListModel, TType>(this HtmlHelper<TViewModel> helper, Expression<Func<TViewModel,TType>> viewModelProperty , Func<TViewModel,IEnumerable<TDropDownListModel>> listProperty, Expression<Func<TDropDownListModel,TType>> valueProperty, Expression<Func<TDropDownListModel,object>> displayProperty, TType selectedValue) 
			where TDropDownListModel:class 
			where TType : struct 
		{
			var model = helper.ViewData.Model;
			var list = listProperty.Invoke(model);
			var selectList = list.ToSelectList(valueProperty, displayProperty, selectedValue);
			return helper.DropDownListFor(viewModelProperty, selectList);
		} 

		public static MvcHtmlString WpcDropDownListFor<TViewModel,TDropDownListModel, TType>(this HtmlHelper<TViewModel> helper, Expression<Func<TViewModel,TType>> viewModelProperty , Func<TViewModel,IEnumerable<TDropDownListModel>> listProperty, Expression<Func<TDropDownListModel,TType>> valueProperty, Expression<Func<TDropDownListModel,object>> displayProperty, bool setSelectedValueFromModel) 
			where TDropDownListModel:class 
			where TType : struct
		{
			var model = helper.ViewData.Model;
			var selectedValue = viewModelProperty.Compile().Invoke(model);
			if(setSelectedValueFromModel)
			{
				return WpcDropDownListFor(helper, viewModelProperty, listProperty, valueProperty, displayProperty, null, selectedValue);
			}
			var list = listProperty.Invoke(model);
			return helper.DropDownListFor(viewModelProperty, list.ToSelectList(valueProperty, displayProperty));
		} 

		public static SelectList ToSelectList<TModel, TType>(this IEnumerable<TModel> list, Expression<Func<TModel,TType>> valueProperty, Expression<Func<TModel,object>> displayProperty) where TModel:class 
		{
			var valuePropertyName = valueProperty.GetName();
			var displayPropertyName = displayProperty.GetName();
			return new SelectList(list, valuePropertyName, displayPropertyName);
		}

		public static SelectList ToSelectList<TModel, TType>(this IEnumerable<TModel> list, Expression<Func<TModel,TType>> valueProperty, Expression<Func<TModel,object>> displayProperty, TType selectedValue) where TModel:class 
		{
			var valuePropertyName = valueProperty.GetName();
			var displayPropertyName = displayProperty.GetName();
			return new SelectList(list, valuePropertyName, displayPropertyName, selectedValue);
		}
	}
}