using System;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class ExpressionExtensions
	{
		public static Expression<Func<TModel, object>> ExpressionFor<TModel>(Expression<Func<TModel, object>> expression) where TModel : class
		{
			return expression;
		}

		public static string GetName<T, TValue>(this Expression<Func<T, TValue>> expression) where T : class
		{
			return new ExpressionNameVisitor().Visit(expression.Body);
		}
	}
}