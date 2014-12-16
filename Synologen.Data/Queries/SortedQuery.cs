using System.Collections.Generic;
using Spinit.Data.NHibernate;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public class SortedQuery<TType> : AbstractListQuery<TType> where TType : class
	{
		protected string OrderBy { get; set; }
		protected bool SortAscending { get; set; }

		public SortedQuery(string orderBy, bool sortAscending = true, ICriteria<TType> criteria = null) 
			: base(criteria)
		{
			OrderBy = orderBy;
			SortAscending = sortAscending;
		}

		public override IEnumerable<TType> Execute()
		{
			var criteria = GetCriteria().Sort(OrderBy, SortAscending);
			var result = criteria.List<TType>();
			return new ExtendedEnumerable<TType>(result, OrderBy, SortAscending);
		}
	}
}