using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public class SortedPagedList<TModel> : PagedList<TModel>, ISortedPagedList<TModel>
	{
		public SortedPagedList(IEnumerable<TModel> result, long total, int page, int pageSize, string orderBy, bool sortAscending) : base(result, total, page, pageSize)
		{
			OrderBy = orderBy;
			SortAscending = sortAscending;
		}

		public SortedPagedList(IPagedList<TModel> result, string orderBy, bool sortAscending) : base(result, result.Total, result.Page, result.PageSize)
		{
			OrderBy = orderBy;
			SortAscending = sortAscending;
		}

		public ISortedPagedList<TOutputModel> ConvertSortedPagedList<TOutputModel>(Converter<TModel, TOutputModel> converter)
		{
			return new SortedPagedList<TOutputModel>(ConvertAll(converter), Total, Page, PageSize, OrderBy, SortAscending);
		}
		public string OrderBy { get; private set; }
		public bool SortAscending { get; private set; }
	}
}