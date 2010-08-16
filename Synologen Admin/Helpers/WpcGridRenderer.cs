using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcGridRenderer<TModel> : HtmlTableGridRenderer<TModel> where TModel : class
	{
		protected const string DefaultTableCssClass = "striped";
		protected const string DefaultHeaderRowCssClass = "header";

		protected override void RenderHeadStart()
		{
			var attributes = GridModel.Sections.HeaderRow.Attributes(new GridRowViewData<TModel>(null, false))
				.SetClass(DefaultHeaderRowCssClass);
			var attributeString = BuildHtmlAttributes(attributes);
			if(attributeString.Length > 0) attributeString = " " + attributeString;
			RenderText(string.Format("<thead><tr{0}>", attributeString));
		}

		protected override void RenderHeaderCellStart(GridColumn<TModel> column) 
		{
			var attributes = new Dictionary<string, object>(column.HeaderAttributes);
			if(IsSortingEnabled && column.Sortable)
			{
				var isSortedByThisColumn = (GridModel.SortOptions.Column == column.Name);
				if (isSortedByThisColumn) 
				{
					var sortClass = GridModel.SortOptions.Direction == SortDirection.Ascending ? "sort_asc" : "sort_desc";
					attributes = attributes.SetClass(sortClass);
				}
			}

			var attrs = BuildHtmlAttributes(attributes);
			if (attrs.Length > 0) attrs = " " + attrs;
			RenderText(string.Format("<th{0}>", attrs));
		}

		protected override void RenderGridStart()
		{
			var attributes = GridModel.Attributes
				.SetClass(DefaultTableCssClass);

			var attrs = BuildHtmlAttributes(attributes);
			if(attrs.Length > 0) attrs = " " + attrs;
			RenderText(string.Format("<table{0}>", attrs));
		}

		//protected override void RenderHeaderText(GridColumn<TModel> column) 
		//{
		//    if(IsSortingEnabled && column.Sortable)
		//    {
		//        string sortColumnName = GenerateSortColumnName(column);

		//        bool isSortedByThisColumn = GridModel.SortOptions.Column == sortColumnName;

		//        var sortOptions = new GridSortOptions 
		//        {
		//            Column = sortColumnName
		//        };

		//        if(isSortedByThisColumn)
		//        {
		//            sortOptions.Direction = (GridModel.SortOptions.Direction == SortDirection.Ascending)
		//                ? SortDirection.Descending 
		//                : SortDirection.Ascending;
		//        }
		//        else //default sort order
		//        {
		//            sortOptions.Direction = GridModel.SortOptions.Direction;
		//        }

		//        var routeValues = CreateRouteValuesForSortOptions(sortOptions, GridModel.SortPrefix);

		//        //Re-add existing querystring
		//        foreach(var key in Context.RequestContext.HttpContext.Request.QueryString.AllKeys)
		//        {
		//            if(! routeValues.ContainsKey(key))
		//            {
		//                routeValues[key] = Context.RequestContext.HttpContext.Request.QueryString[key];
		//            }
		//        }

		//        var link = HtmlHelper.GenerateLink(Context.RequestContext, RouteTable.Routes, column.DisplayName, null, null, null, routeValues, null);
		//        RenderText(link);
		//    }
		//    else
		//    {
		//        base.RenderHeaderText(column);
		//    }
		//}

		//private RouteValueDictionary CreateRouteValuesForSortOptions(GridSortOptions sortOptions, string prefix)
		//{
		//    if(string.IsNullOrEmpty(prefix))
		//    {
		//        return new RouteValueDictionary(sortOptions);
		//    }

		//    //There must be a nice way to do this...
		//    return new RouteValueDictionary(new Dictionary<string, object>()
		//    {
		//        { prefix + "." + "Column", sortOptions.Column },
		//        { prefix + "." + "Direction", sortOptions.Direction }
		//    });
		//}

		//protected virtual string GenerateSortColumnName(GridColumn<TModel> column)
		//{
		//    //Use the explicit sort column name if specified. If not possible, fall back to the property name.
		//    //If the property name cannot be inferred (ie the expression is not a MemberExpression) then try the display name instead.
		//    return column.Name ?? column.DisplayName;
		//}

		private static string BuildHtmlAttributes(ICollection<KeyValuePair<string, object>> attributes)
		{
			if(attributes == null || attributes.Count == 0) return string.Empty;
			const string attributeFormat = "{0}=\"{1}\"";
			return string.Join(" ",attributes.Select(pair => string.Format(attributeFormat, pair.Key, pair.Value)).ToArray());
		}
	}
}