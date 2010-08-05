using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcGrid<TModel> : MvcContrib.UI.Grid.Grid<TModel> where TModel : class
	{
		public WpcGrid(IEnumerable<TModel> dataSource, TextWriter writer, ViewContext context) : base(dataSource, writer, context)
		{
			if (dataSource is ISortedPagedList<TModel>)
			{
				var list = (ISortedPagedList<TModel>) dataSource;
				Sort(list.GetGridSortOptions());
			}
			RenderUsing(new WpcGridRenderer<TModel>());
		}
	}
}