using System;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public static class WpcPagerExtensions {
		/// <summary>
		/// Creates a WPC pager component using the item from the viewdata with the specified key as the datasource.
		/// </summary>
		/// <param name="helper">The HTML Helper</param>
		/// <param name="viewDataKey">The viewdata key</param>
		/// <returns>A WpcPager component</returns>
		public static WpcPager WpcPager(this HtmlHelper helper, string viewDataKey) {
			var dataSource = helper.ViewContext.ViewData.Eval(viewDataKey) as IPagedList;

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
		public static WpcPager WpcPager(this HtmlHelper helper, IPagedList pagination) {
			return new WpcPager(pagination, helper.ViewContext.HttpContext.Request);
		}
	}
}