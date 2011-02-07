using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using MvcContrib.UI.Grid;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcGrid<TModel> : Grid<TModel> where TModel : class
	{
		public WpcGrid(IEnumerable<TModel> dataSource, TextWriter writer, ViewContext context) : base(dataSource, writer, context)
		{
			//if (dataSource is IExtendedEnumerable<TModel>)
			//{
			var sortOptions = new GridSortOptions
			{
				Column = GetSortColumn(context),
				Direction = GetSortDirection(context)
			};
			Sort(sortOptions);
			//}
			RenderUsing(new WpcGridRenderer<TModel>());
		}

		private static MvcContrib.Sorting.SortDirection GetSortDirection(ControllerContext context)
		{
			var directionValue = context.RequestContext.HttpContext.Request["Direction"];
			if(directionValue == null) return MvcContrib.Sorting.SortDirection.Ascending;
			return (MvcContrib.Sorting.SortDirection) Enum.Parse(typeof (MvcContrib.Sorting.SortDirection), directionValue);
		}

		private static string GetSortColumn(ControllerContext context)
		{
			return context.RequestContext.HttpContext.Request["Column"] ?? String.Empty;
		}

	}
}