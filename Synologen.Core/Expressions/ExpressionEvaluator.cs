using System;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Core.Expressions
{
	public class ExpressionEvaluator : ExpressionVisitor
	{
    	protected readonly Expression Expression;
		protected Func<Expression, bool> MatchExpression;

		public ExpressionEvaluator(Expression expression)
    	{
    		Expression = expression;
    	}

		public override Expression Visit(Expression node)
		{
            if (node == null) return null;
            return MatchExpression(node) ? Resolve(node) : base.Visit(node);
		}

		public Expression Evaluate(Func<Expression, bool> matchExpression)
		{
		    MatchExpression = matchExpression;
		    return Visit(Expression);
		}

		public Expression EvaluateConstantsAndParameters()
		{
			MatchExpression = e =>
		    {
				if (e is UnaryExpression) return true;
				if (e is ConstantExpression) return true;
				return e.NodeType == ExpressionType.MemberAccess;
		    };
		    return Visit(Expression);
		}

		protected virtual Expression Resolve(Expression node)
		{
            if (node.NodeType == ExpressionType.Constant) return node;
            var type = node.Type;
            if (type.IsValueType)
            {
                node = Expression.Convert(node, typeof(object));
            }
            var lambda = Expression.Lambda<Func<object>>(node);
            var fn = lambda.Compile();
            return Expression.Constant(fn(), type);
		}
	}
}