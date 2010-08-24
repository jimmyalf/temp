using System;
using NHibernate;
using NHibernate.Criterion;
using System.Linq.Expressions;
using NHibernate.Impl;
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
			return ApplyFilter(expression, filterEntity, MatchMode.Anywhere);
		}

		public static AbstractCriterion ApplyFilter<TModel>(Expression<Func<TModel,string>> expression, string filterEntity, MatchMode matchMode) where TModel : class
		{
			var propertyName = expression.GetName();
			return Restrictions.InsensitiveLike(propertyName, filterEntity, matchMode);
		}

        public static ICriteria FilterName<TModel>(this ICriteria criteria, Expression<Func<TModel,string>> expression, string filterEntity) where TModel : class
		{
        	return FilterName(criteria, expression, filterEntity, MatchMode.Anywhere);
		}

		public static ICriteria FilterName<TModel>(this ICriteria criteria, Expression<Func<TModel,string>> expression, string filterEntity, MatchMode matchMode) where TModel : class
		{
        	return criteria.Add(ApplyFilter(expression, filterEntity, matchMode));
		}

		public static Junction ApplyFilterInJunction<TModel>(this Junction junction, Expression<Func<TModel,string>> expression, string filterEntity) where TModel : class
		{
			return ApplyFilterInJunction(junction, expression, filterEntity, MatchMode.Anywhere);
		}

		public static Junction ApplyFilterInJunction<TModel>(this Junction junction, Expression<Func<TModel,string>> expression, string filterEntity, MatchMode matchMode) where TModel : class
		{
			return junction.Add(ApplyFilter(expression, filterEntity, matchMode));
		}

		//public static ICriteria<TEntity> ToGenericCriteria<TEntity>(this ISession session)
		//{
		//    return new CriteriaImpl<TEntity>(session);
		//}

		
	}

	//public class CriteriaImpl<TEntity> : CriteriaImpl, ICriteria<TEntity>
	//{
	//    public CriteriaImpl(ISession session) : base(typeof(TEntity), session.GetSessionImplementation()) {  }
	//}

	//public interface ICriteria<TEntity> : ICriteria{}
}