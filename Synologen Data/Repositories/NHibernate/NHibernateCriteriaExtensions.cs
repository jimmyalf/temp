using System;
using NHibernate;
using NHibernate.Criterion;
using System.Linq.Expressions;

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
		public static ICriteria SetAlias<TModel>(this ICriteria criteria, Expression<Func<TModel,object>> expression)
		{
			var propertyName = ((MemberExpression)expression.Body).Member.Name;
			return criteria.CreateAlias(propertyName, propertyName);
		}
		public static ICriteria SetAlias<TModel>(this ICriteria criteria, Expression<Func<TModel,object>> expression, string aliasName)
		{
			var propertyName = ((MemberExpression)expression.Body).Member.Name;
			return criteria.CreateAlias(propertyName, aliasName);
		}
	}
}