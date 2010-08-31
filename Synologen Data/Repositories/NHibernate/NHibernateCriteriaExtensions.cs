using System;
using NHibernate;
using NHibernate.Criterion;
using System.Linq.Expressions;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public static class NHibernateCriteriaExtensions
	{

		public static ICriteria FilterEqual<TModel, TType>(this ICriteria criteria, Expression<Func<TModel,TType>> expression, TType filterEntity) where TModel : class
		{
			var propertyName = expression.GetName();
			return criteria.Add(Restrictions.Eq(propertyName, filterEntity));
		}
		
	}

}