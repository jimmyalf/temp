using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public class PagedList<TModel> : List<TModel>, IPagedList<TModel>
	{
		public PagedList(IEnumerable<TModel> result, long total, int page, int pageSize)
		{
			Total = total;
			Page = page;
			PageSize = pageSize;
			NumberOfPages = GetNumberOfPages();
			AddRange(result);
			HasPreviousPage = (Page > 1);
			HasNextPage = (Page < NumberOfPages);
		}

		public long Total { get; private set; }
		public int Page { get; private set; }
		public int PageSize { get; private set; }
		public int NumberOfPages { get; private set; }
		public bool HasPreviousPage { get; private set; }
		public bool HasNextPage { get; private set; }

		public IPagedList<TOutputModel> ConvertList<TOutputModel>(Converter<TModel, TOutputModel> converter) 
		{
			return new PagedList<TOutputModel>(ConvertAll(converter), Total, Page, PageSize);
		}

		private int GetNumberOfPages()
		{
			var calculatedNumberOfPages = (int) (Total/PageSize);
			var EmptyOrUnevenPage = (calculatedNumberOfPages == 0 || Total%PageSize > 0);
			return EmptyOrUnevenPage ? calculatedNumberOfPages + 1 : calculatedNumberOfPages;
		}
	}
}