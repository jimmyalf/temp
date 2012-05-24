using System.Collections.Generic;
using Spinit.Data.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public class PagedQuery<TType> : AbstractListQuery<TType> where TType : class
	{
		protected int Page { get; set; }
		protected int PageSize { get; set; }

		public PagedQuery(int page, int pageSize, ICriteria<TType> criteria = null) : base(criteria)
		{
			Page = page;
			PageSize = pageSize;
			Criteria = criteria;
		}

		public override IEnumerable<TType> Execute()
		{
			long count;
			var criteria = GetCriteria();
			var result = GetPagedResult(criteria, out count);
			return new ExtendedEnumerable<TType>(result, count, Page, PageSize);
		}
	}
}