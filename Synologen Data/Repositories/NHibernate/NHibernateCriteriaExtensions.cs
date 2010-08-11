using System;
using NHibernate;
using NHibernate.Criterion;
using System.Linq.Expressions;
using Spinit.Wpc.Synologen.Core.Extensions;

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

		public static ICriteria SetAlias<TModel>(this ICriteria criteria, Expression<Func<TModel,object>> expression) where TModel : class
		{
			var propertyName = expression.GetName();
			return criteria.CreateAlias(propertyName, propertyName);
		}
		public static ICriteria SetAlias<TModel>(this ICriteria criteria, Expression<Func<TModel,object>> expression, string aliasName) where TModel : class
		{
			var propertyName = expression.GetName();
			return criteria.CreateAlias(propertyName, aliasName);
		}

		public static AbstractCriterion ApplyFilter<TModel>(Expression<Func<TModel,string>> expression, string filterEntity) where TModel : class
		{
			var propertyName = expression.GetName();
			return Restrictions.InsensitiveLike(propertyName, String.Format("%{0}%", filterEntity));
		}

		public static Junction ApplyFilterInJunction<TModel>(this Junction junction, Expression<Func<TModel,string>> expression, string filterEntity) where TModel : class
		{
			var propertyName = expression.GetName();
			return junction.Add(Restrictions.InsensitiveLike(propertyName, String.Format("%{0}%", filterEntity)));
		}
	}
}