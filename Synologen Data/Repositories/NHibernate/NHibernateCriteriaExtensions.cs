using NHibernate;
using NHibernate.Criterion;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public static class NHibernateCriteriaExtensions
	{
		public static ICriteria Page(this ICriteria criteria, int pageNumber, int pageSize) {
			if (pageNumber > 0) pageNumber = pageNumber - 1;
			return criteria
				.SetFirstResult(pageNumber * pageSize)
				.SetMaxResults(pageSize);
		}

		public static ICriteria Sort(this ICriteria criteria , string propertyName, bool sortAscending) {
			if(string.IsNullOrEmpty(propertyName)) return criteria;
			return sortAscending ? criteria.AddOrder(Order.Asc(propertyName)) : criteria.AddOrder(Order.Desc(propertyName));
		}

		public static ICriteria GetCount(this ICriteria criteria)
		{
			criteria.SetFirstResult(0)
				.SetMaxResults(-1)
				.SetProjection(Projections.RowCountInt64());
			criteria.ClearOrders();
			return criteria;
		}
	}
}