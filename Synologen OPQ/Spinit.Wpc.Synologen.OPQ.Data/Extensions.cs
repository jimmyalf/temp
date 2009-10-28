using System;
using System.Linq;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Opq.Data
{
	public static class Extensions
	{
		public static IQueryable<T> AddEqualityCondition<T, TV> (this IQueryable<T> queryable, string propertyName, TV propertyValue)
		{
			ParameterExpression pe = Expression.Parameter (typeof (T), "p");

			IQueryable<T> x = queryable.Where (
			  Expression.Lambda<Func<T, bool>> (
				Expression.Equal (Expression.Property (
				  pe,
				  typeof (T).GetProperty (propertyName)),
				  Expression.Constant (propertyValue, typeof (TV)),
				  false,
				  typeof (T).GetMethod ("op_Equality")),
			  new [] { pe }));

			return x;
		}
	}
}