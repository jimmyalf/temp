using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class WpcPagerExtensions {
		/// <summary>
		/// Creates a WPC pager component using the item from the viewdata with the specified key as the datasource.
		/// </summary>
		/// <param name="helper">The HTML Helper</param>
		/// <param name="viewDataKey">The viewdata key</param>
		/// <returns>A WpcPager component</returns>
		public static WpcPager<TModel> WpcPager<TModel>(this HtmlHelper helper, string viewDataKey) where TModel : class
		{
			var dataSource = helper.ViewContext.ViewData.Eval(viewDataKey) as IEnumerable<TModel>;

			if (dataSource == null) {
				throw new InvalidOperationException(string.Format("Item in ViewData with key '{0}' is not an IPagedList.", viewDataKey));
			}

			return helper.WpcPager(dataSource);
		}

		/// <summary>
		/// Creates a WPC pager component using the specified IPagination as the datasource.
		/// </summary>
		/// <param name="helper">The HTML Helper</param>
		/// <param name="pagination">The datasource</param>
		/// <returns>A WpcPager component</returns>
		public static WpcPager<TModel> WpcPager<TModel>(this HtmlHelper helper, IEnumerable<TModel> pagination) where TModel : class {
			return new WpcPager<TModel>(pagination, helper.ViewContext.HttpContext.Request);
		}
	}
}