using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NHibernate;
using Spinit.Wpc.Synologen.Data.Commands;
using Spinit.Wpc.Synologen.Data.Queries;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
    	private ISession _session { get; set; }
		
    	public Action<LambdaExpression> SessionWithoutResultOverride { get; set; }
		public Func<LambdaExpression, Type, object> SessionWithResultOverride { get; set; }

		public Action<Command> ExecuteCommandOverride { get; set; }
		public Func<Command, Type, object> ExecuteCommandWithResultOverride { get; set; }

		public Func<Query, Type, object> QueryOverride { get; set; }

    	public BaseController(ISession session)
    	{
    		_session = session;
    	}

    	protected TResult Query<TResult>(Query<TResult> query)
		{
			if (QueryOverride != null) return (TResult) QueryOverride(query, typeof (TResult));
			query.Session = _session;
			return query.Execute();
		}

		protected void Execute(Command command)
		{
			if(ExecuteCommandOverride != null) ExecuteCommandOverride(command);
			command.Session = _session;
			command.Execute();
		}

		protected TResult Execute<TResult>(Command<TResult> command)
		{
			if (ExecuteCommandWithResultOverride != null) return (TResult) ExecuteCommandWithResultOverride(command, typeof (TResult));
			command.Session = _session;
			command.Execute();
			return command.Result;
		}

		protected TResult WithSession<TResult>(Expression<Func<ISession, TResult>> expression)
		{
			if(SessionWithResultOverride != null) return (TResult) SessionWithResultOverride(expression, typeof(TResult));
			var function = expression.Compile();
			return function(_session);
		}

		protected void WithSession<TResult>(Expression<Action<ISession>> expression)
		{
			if(SessionWithoutResultOverride != null) SessionWithoutResultOverride(expression);
			var action = expression.Compile();
			action(_session);
		}
    }
}