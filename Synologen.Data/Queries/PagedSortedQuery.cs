using System.Collections.Generic;
using Spinit.Data.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public class PagedSortedQuery<TType> : PagedQuery<TType> where TType : class
	{
		protected string OrderBy { get; set; }
		protected bool SortAscending { get; set; }

		public PagedSortedQuery(int page, int pageSize, string orderBy, bool sortAscending = true, ICriteria<TType> criteria = null) 
			: base(page, pageSize, criteria)
		{
			OrderBy = orderBy;
			SortAscending = sortAscending;
		}

		public override IEnumerable<TType> Execute()
		{
			long count;
			var criteria = GetCriteria().Sort(OrderBy, SortAscending);
			var result = GetPagedResult(criteria, out count);
			return new ExtendedEnumerable<TType>(result, count, Page, PageSize, OrderBy, SortAscending);
		}
	}
}