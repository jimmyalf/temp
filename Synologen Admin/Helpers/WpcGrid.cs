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
				var list = (ISortedPagedList<TModel>) dataSource;
				Sort(GetGridSortOptions(list));
			}
			RenderUsing(new WpcGridRenderer<TModel>());
		}

		protected static GridSortOptions GetGridSortOptions(ISortedPagedList list)
		{
			return new GridSortOptions
			{
				Column = list.OrderBy,
				Direction = (list.SortAscending) ? SortDirection.Ascending : SortDirection.Descending
			};
		}

	}
}