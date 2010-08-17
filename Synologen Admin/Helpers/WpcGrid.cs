using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcGrid<TModel> : Grid<TModel> where TModel : class
	{
		public WpcGrid(IEnumerable<TModel> dataSource, TextWriter writer, ViewContext context) : base(dataSource, writer, context)
		{
			if (dataSource is ISortedPagedList<TModel>)
			{
				var sortOptions = new GridSortOptions
				{
					Column = GetSortColumn(context),
					Direction = GetSortDirection(context)
				};
				Sort(sortOptions);
			}
			RenderUsing(new WpcGridRenderer<TModel>());
		}

		private static SortDirection GetSortDirection(ControllerContext context)
		{
			var directionValue = context.RequestContext.HttpContext.Request["Direction"];
			if(directionValue == null) return SortDirection.Ascending;
			return (SortDirection) Enum.Parse(typeof (SortDirection), directionValue);
		}

		private static string GetSortColumn(ControllerContext context)
		{
			return context.RequestContext.HttpContext.Request["Column"] ?? String.Empty;
		}

	}
}