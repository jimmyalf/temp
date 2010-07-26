using NHibernate;

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
	}
}