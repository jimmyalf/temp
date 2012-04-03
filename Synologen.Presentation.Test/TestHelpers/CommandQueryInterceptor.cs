using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Expressions;

namespace Spinit.Wpc.Synologen.Presentation.Test.TestHelpers
{
	public class CommandQueryInterceptor<TSession>
	{
		private readonly IDictionary<Type, object> _queryInterceptors;
		private readonly IDictionary<Type, object> _commandInterceptors;
		private readonly IDictionary<Type, Action> _commandActionInterceptors; 
		private readonly IDictionary<string, object> _sessionFunctionInterceptors;
		private readonly IDictionary<Type, object> _sessionFunctionReturnInterceptors;
		private readonly IDictionary<string, Action> _sessionActionInterceptors;

		public CommandQueryInterceptor()
		{
			_queryInterceptors = new Dictionary<Type, object>();
			_sessionFunctionInterceptors = new Dictionary<string, object>();
			_sessionFunctionReturnInterceptors = new Dictionary<Type, object>();
			_sessionActionInterceptors = new Dictionary<string, Action>();
			_commandInterceptors = new Dictionary<Type, object>();
			_commandActionInterceptors = new Dictionary<Type, Action>();
		}

		#region Setup
		public void SetupQueryResult(Type queryType, object queryResult)
		{
			_queryInterceptors.Add(queryType, queryResult);
		}

		public void SetupQueryResult<TQuery>(object queryResult) where TQuery : IQuery
		{
			SetupQueryResult(typeof (TQuery), queryResult);
		}

		public void SetupSessionResult<TResult>(Expression<Func<TSession, TResult>> sessionExpression, object result)
		{
			var key = GetExpressionKey(sessionExpression);
			_sessionFunctionInterceptors.Add(key, result);
		}

		public void SetupSessionAction(Expression<Action<TSession>> sessionExpression, Action action)
		{
			var key = GetExpressionKey(sessionExpression);
			_sessionActionInterceptors.Add(key, action);
		}

		public void SetupSessionReturning(Type returnType, object result)
		{
			_sessionFunctionReturnInterceptors.Add(returnType, result);
		}

		public void SetupSessionReturning<TResultType>(object result)
		{
			SetupSessionReturning(typeof (TResultType), result);
		}

		public void SetupCommandResult(Type type, object result)
		{
			_commandInterceptors.Add(type, result);
		}

		public void SetupCommandResult<TCommand, TReturnType>(object result) where TCommand : ICommand<TReturnType>
		{
			SetupCommandResult(typeof (TCommand), result);
		}

		public void SetupCommandAction(Type type, Action action)
		{
			_commandActionInterceptors.Add(type, action);
		}

		public void SetupCommandAction<TCommand>(Action action) where TCommand : ICommand
		{
			_commandActionInterceptors.Add(typeof(TCommand), action);
		}

		#endregion

		#region Get

		public virtual object GetCommandResult(ICommand command, Type resultType)
		{
			return _commandInterceptors.ContainsKey(command.GetType())
			       	? _commandInterceptors[command.GetType()]
			       	: GetDefaultItem(resultType);
		}

		public virtual void GetCommandAction(ICommand command)
		{
			var key = command.GetType();
			if (_commandActionInterceptors.ContainsKey(key))
			{
				_commandActionInterceptors[key].Invoke();
			}
		}

		public virtual object GetQueryResult(IQuery query, Type resultType)
		{
			return _queryInterceptors.ContainsKey(query.GetType())
			       	? _queryInterceptors[query.GetType()]
			       	: GetDefaultItem(resultType);
		}

		public virtual object GetSessionResult(Expression expression, Type resultType)
		{
			var key = GetExpressionKey(expression);
			if(_sessionFunctionInterceptors.ContainsKey(key))
			{
				return _sessionFunctionInterceptors[key];
			}
			return _sessionFunctionReturnInterceptors.ContainsKey(resultType) 
			       	? _sessionFunctionReturnInterceptors[resultType] 
			       	: GetDefaultItem(resultType);
		}

		public virtual void GetSessionAction(Expression expression)
		{
			var key = GetExpressionKey(expression);
			if (_sessionActionInterceptors.ContainsKey(key))
			{
				_sessionActionInterceptors[key].Invoke();
			}
		}

		#endregion

		protected virtual object GetDefaultItem(Type type)
		{
			//if return type is IEnumerable, return an empty ienumerable
			if(type.GetInterfaces().Contains(typeof(IEnumerable)))
			{
				var genericType = type.GetGenericArguments()[0];
				return Array.CreateInstance(genericType, 0);
			}
			//else return null
			return null;
		}

		protected virtual string GetExpressionKey(Expression expression)
		{
			//Try evaluate variables etc before creating a key
			var evaluatedExpression = new ExpressionEvaluator(expression).EvaluateConstantsAndParameters();
			//Try replace lambda parameter name with an common name (session)
			return new ExpressionReplacer(evaluatedExpression)
				.Replace(x => x is ParameterExpression && x.Type == typeof(TSession))
				.With(Expression.Parameter(typeof(TSession), "session"))
				.ToString();
		}
	}
}