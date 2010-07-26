using System;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Synologen.Core.Persistence;
using Expression=NHibernate.Criterion.Expression;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public abstract class NHibernateActionCriteriaConverter<TCriteriaSource, TModel> : IActionCriteriaConverter<TCriteriaSource, ICriteria>
		where TModel : class
		where TCriteriaSource : IActionCriteria
	{
		private const string Alias = "{alias}";
		private const string DateSqlFormat = "({0}({2}.{1}) = ?)";

		public abstract ICriteria Convert(TCriteriaSource source);

		protected string Property(Expression<Func<TModel, object>> expression)
		{
			return PersistenceHelper.GetName(expression);
		}

		protected AbstractCriterion Year(Expression<Func<TModel, object>> expression, int value)
		{
			var dateCriteriaSql = string.Format(DateSqlFormat, "YEAR", PersistenceHelper.GetName(expression), Alias);
			return Expression.Sql(dateCriteriaSql, value, NHibernateUtil.Int32);
		}

		protected AbstractCriterion Month(Expression<Func<TModel, object>> expression, int value)
		{
			var dateCriteriaSql = string.Format(DateSqlFormat, "MONTH", PersistenceHelper.GetName(expression), Alias);
			return Expression.Sql(dateCriteriaSql, value, NHibernateUtil.Int32);
		}

		protected AbstractCriterion Day(Expression<Func<TModel, object>> expression, int value)
		{
			var dateCriteriaSql = string.Format(DateSqlFormat, "DAY", PersistenceHelper.GetName(expression), Alias);
			return Expression.Sql(dateCriteriaSql, value, NHibernateUtil.Int32);
		}
	}
}