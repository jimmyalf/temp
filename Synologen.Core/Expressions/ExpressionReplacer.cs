using System;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Core.Expressions
{
    public class ExpressionReplacer
    {
    	private readonly Expression _expression;

    	public ExpressionReplacer(Expression expression)
    	{
    		_expression = expression;
    	}

        public ReplaceHelper Replace(Func<Expression,bool> searchForExpression)
        {
        	return new ReplaceHelper(_expression, searchForExpression);
        }
    }

	public class ReplaceHelper : ExpressionVisitor
	{
		private readonly Expression _expression;
		private readonly Func<Expression, bool> _searchForExpression;
		private Expression _replacement;

		public ReplaceHelper(Expression expression, Func<Expression,bool> searchForExpression)
		{
			_expression = expression;
			_searchForExpression = searchForExpression;
		}

		public Expression With(Expression replacement)
		{
			_replacement = replacement;
			return Visit(_expression);
		}

		public override Expression Visit(Expression exp)
		{
        	return _searchForExpression(exp) ? _replacement : base.Visit(exp);
		}
	}
}
