using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Extensions;

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

		public MatchMode MatchMode { get; private set; }

		public override AbstractCriterion GetCriterion(object defaultFilter)
		{
			var filter =  (Filter != null) 
				? Filter.ToString()
				: (defaultFilter != null) ? defaultFilter.ToString() : null;
			return Restrictions.InsensitiveLike(PropertyName, filter, MatchMode);
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

	public abstract class AbstractFilter
	{
		public abstract AbstractCriterion GetCriterion(object defaultFilter);
		public string PropertyName { get; protected set; }
		public object Filter { get; protected set; }
	}

	public class SynologenFilterByBuilder<TEntity> : IEnumerable<AbstractFilter>  where TEntity : class 
	{
		private readonly IList<AbstractFilter> _items = new List<AbstractFilter>(); 

		public void Like(Expression<Func<TEntity, string>> expression, string filter = null, MatchMode matchMode = null)
		{
			_items.Add(new LikeFilter<TEntity>(expression, filter, matchMode));
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

		public void IfInt(string input, Action<int> action)
		{
			int value;
			if(int.TryParse(input,out value))
			{
				action(value);
			}
		}


		IEnumerator<AbstractFilter> IEnumerable<AbstractFilter>.GetEnumerator()
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
			foreach (AbstractFilter filterBy in builder)
			{
				disjunction.Add(filterBy.GetCriterion(defaultFilter));
			}
			return (ICriteria<TEntity>) criteria.Add(disjunction);
		}

		public static ICriteria<TEntity> SynologenFilterByAll<TEntity>(this ICriteria<TEntity> criteria, Action<SynologenFilterByBuilder<TEntity>> filterBuilder, object defaultFilter = null) where TEntity : class
		{
			var builder = new SynologenFilterByBuilder<TEntity>();
			filterBuilder(builder);
			foreach (AbstractFilter filter in builder)
			{
				criteria.Add(filter.GetCriterion(defaultFilter));
			}
			return criteria;
		}
	}
}