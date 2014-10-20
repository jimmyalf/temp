using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Type;
using Spinit.Data.NHibernate;
using Spinit.Extensions;
using Expression = NHibernate.Criterion.Expression;

namespace Spinit.Wpc.Synologen.Data.Extensions
{
	public class LikeFilter<TEntity> : AbstractFilter where TEntity : class 
	{
		public LikeFilter(Expression<Func<TEntity, string>> expression, string filter = null, MatchMode matchMode = null) 
			: this(expression.GetName(), filter, matchMode){}
		public LikeFilter(string aliasPath, string filter = null, MatchMode matchMode = null)
		{
			PropertyName = aliasPath;
			Filter = filter;
			MatchMode = matchMode ?? MatchMode.Anywhere;
		}

		public LikeFilter(IProjection projection, string filter = null, MatchMode matchMode = null)
		{
			Projection = projection;
			Filter = filter;
			MatchMode = matchMode ?? MatchMode.Anywhere;
		}

		protected IProjection Projection { get; set; }

		public MatchMode MatchMode { get; private set; }

		public override AbstractCriterion GetCriterion(object defaultFilter)
		{
			var filter =  (Filter != null) 
				? Filter.ToString()
				: (defaultFilter != null) ? defaultFilter.ToString() : null;
			return Projection != null 
				? Restrictions.InsensitiveLike(Projection, filter, MatchMode) 
				: Restrictions.InsensitiveLike(PropertyName, filter, MatchMode);
		}
	}

	public class EqualFilter<TEntity> : AbstractFilter where TEntity : class
	{
		public EqualFilter(Expression<Func<TEntity, object>> expression, object filter = null) : this(expression.GetName(), filter) { }

		public EqualFilter(string aliasPath, object filter = null)
		{
			PropertyName = aliasPath;
			Filter = filter;
		}

		public override AbstractCriterion GetCriterion(object defaultFilter)
		{
			var filter = Filter ?? defaultFilter;
			return Restrictions.Eq(PropertyName, filter);
		}
	}

	public class SqlFilter : IFilter
	{
		public string Sql { get; set; }
		public object Filter { get; set; }

		public SqlFilter(string sql, object filter = null)
		{
			Sql = sql;
			Filter = filter;
		}

		public AbstractCriterion GetCriterion(object defaultFilter)
		{
			var filter = Filter ?? defaultFilter;
			return Expression.Sql(String.Format(Sql, filter));
		}
	}

	public class CriteriaFilter : IFilter
	{
		public AbstractCriterion Criteria { get; set; }

		public CriteriaFilter(AbstractCriterion criteria)
		{
			Criteria = criteria;
		}

		public AbstractCriterion GetCriterion(object defaultFilter)
		{
			return Criteria;
		}
	}

	public abstract class AbstractFilter : IFilter
	{
		public abstract AbstractCriterion GetCriterion(object defaultFilter);
		public string PropertyName { get; protected set; }
		public object Filter { get; protected set; }
	}

	public interface IFilter
	{
		AbstractCriterion GetCriterion(object defaultFilter);
	}

	public class FilterExtender<TEntity>  where TEntity : class 
	{
	    public ConcatBuilder<TEntity> Concat(Expression<Func<TEntity, object>> expression)
	    {
	        return new ConcatBuilder<TEntity>().And(new ConcatParameter<TEntity>(expression));
	    }

	    public ConcatBuilder<TEntity> Concat(string constant)
	    {
	        return new ConcatBuilder<TEntity>().And(new ConcatParameter<TEntity>(constant));
	    }

	    public ConcatBuilder<TEntity> Concat(IConcatParameter<TEntity> parameter)
	    {
	        return new ConcatBuilder<TEntity>().And(parameter);
	    }		
	}

	public class SynologenFilterByBuilder<TEntity> : IEnumerable<IFilter>  where TEntity : class 
	{
		private readonly IList<IFilter> _items = new List<IFilter>(); 

		public void Like(Expression<Func<TEntity, string>> expression, string filter = null, MatchMode matchMode = null)
		{
			_items.Add(new LikeFilter<TEntity>(expression, filter, matchMode));
		}

		public void Like(Func<FilterExtender<TEntity>,IProjection> func, string filter = null, MatchMode matchMode = null)
		{
			var filterExtender = new FilterExtender<TEntity>();
			var projection = func(filterExtender);
			_items.Add(new LikeFilter<TEntity>(projection, filter, matchMode));
		}

		public void Like(string aliasPath, string filter = null, MatchMode matchMode = null)
		{
			_items.Add(new LikeFilter<TEntity>(aliasPath, filter, matchMode));
		}

		public void Equal(Expression<Func<TEntity, object>> expression, object filter = null)
		{
			_items.Add(new EqualFilter<TEntity>(expression, filter));
		}

		public void Equal(string aliasPath, object filter = null)
		{
			_items.Add(new EqualFilter<TEntity>(aliasPath, filter));
		}

		public void Custom(IFilter filter)
		{
			_items.Add(filter);
		}

		public void IfInt(string input, Action<int> action)
		{
			int value;
			if(int.TryParse(input,out value))
			{
				action(value);
			}
		}

		public void If(bool condition, Action action)
		{
			if(condition) { action(); }
		}


		IEnumerator<IFilter> IEnumerable<IFilter>.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return _items.GetEnumerator();
		}
	}

	public static class CriteriaExtensions
	{
		public static ICriteria<TEntity> SynologenFilterByAny<TEntity>(this ICriteria<TEntity>  criteria, Action<SynologenFilterByBuilder<TEntity>> filterBuilder, object defaultFilter = null) where TEntity : class
		{
			var builder = new SynologenFilterByBuilder<TEntity>();
			filterBuilder(builder);
			var disjunction = Restrictions.Disjunction();
			foreach (IFilter filterBy in builder)
			{
				disjunction.Add(filterBy.GetCriterion(defaultFilter));
			}
			return (ICriteria<TEntity>) criteria.Add(disjunction);
		}

		public static ICriteria<TEntity> SynologenFilterByAll<TEntity>(this ICriteria<TEntity> criteria, Action<SynologenFilterByBuilder<TEntity>> filterBuilder, object defaultFilter = null) where TEntity : class
		{
			var builder = new SynologenFilterByBuilder<TEntity>();
			filterBuilder(builder);
			foreach (IFilter filter in builder)
			{
				criteria.Add(filter.GetCriterion(defaultFilter));
			}
			return criteria;
		}
		public static ICriteria<TEntity> SetFetchMode<TEntity>(this ICriteria<TEntity> criteria, Expression<Func<TEntity,object>> property, FetchMode fetchMode) where TEntity : class
		{
			return (ICriteria<TEntity>) criteria.SetFetchMode(property.GetName(), fetchMode);
		}
	}

	public class ConcatBuilder<TType> : SimpleProjection where TType : class
	{
		protected IList<IConcatParameter<TType>> _parameters;

		public ConcatBuilder()
		{
			_parameters = new List<IConcatParameter<TType>>();
		}

		public virtual ConcatBuilder<TType> And(Expression<Func<TType, object>> expression)
		{
			_parameters.Add(new ConcatParameter<TType>(expression));
			return this;
		}

		public virtual ConcatBuilder<TType> And(string constant)
		{
			_parameters.Add(new ConcatParameter<TType>(constant));
			return this;
		}

		public virtual ConcatBuilder<TType> And(IConcatParameter<TType> parameter)
		{
			_parameters.Add(parameter);
			return this;
		}

		protected virtual IProjection ToProjection()
		{
			var projections = _parameters.Select(x => x.Projection).ToArray();
			return Projections.SqlFunction("concat", NHibernateUtil.String, projections);
		}

		public override SqlString ToSqlString(ICriteria criteria, int position, ICriteriaQuery criteriaQuery, IDictionary<string, NHibernate.IFilter> enabledFilters)
		{
			return ToProjection().ToSqlString(criteria, position, criteriaQuery, enabledFilters);
		}

		public override IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return ToProjection().GetTypes(criteria, criteriaQuery);
		}

		public override bool IsGrouped
		{
			get { return ToProjection().IsGrouped; }
		}

		public override bool IsAggregate
		{
			get { return ToProjection().IsAggregate; }
		}

		public override SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, NHibernate.IFilter> enabledFilters)
		{
			return ToProjection().ToGroupSqlString(criteria, criteriaQuery, enabledFilters);
		}
	}

	public class ConcatParameter<TType> : IConcatParameter<TType> where TType : class
	{
		public IProjection Projection { get; private set; }
		public ConcatParameter(Expression<Func<TType, object>> expression)
		{
			Projection = Projections.Property(expression.GetName());
		}
		public ConcatParameter(string value)
		{
			Projection = Projections.Constant(value);
		}
	}

	public interface IConcatParameter<TType> where TType : class
	{
		IProjection Projection { get; }
	}
}