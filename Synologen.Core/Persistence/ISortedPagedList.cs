using System;

namespace Spinit.Wpc.Synologen.Core.Persistence
{

	public interface ISortedPagedList<TModel> : ISortedPagedList, IPagedList<TModel>
	{
		ISortedPagedList<TOutputModel> ConvertSortedPagedList<TOutputModel>(Converter<TModel, TOutputModel> converter);	
	}

	public interface ISortedPagedList : IPagedList {
		string OrderBy{ get;}
		bool SortAscending { get; }
	}
}