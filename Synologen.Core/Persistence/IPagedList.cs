using System;
using System.Collections;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface IPagedList<TModel> : IPagedList, IEnumerable<TModel> {
		IPagedList<TOutputModel> ConvertList<TOutputModel>(Converter<TModel, TOutputModel> converter);
	}

	public interface IPagedList : IEnumerable {
		long Total { get; }
		int Page { get; }
		int PageSize { get; }
		int NumberOfPages { get; }
		bool HasPreviousPage { get; }
		bool HasNextPage  { get; }
	}
}