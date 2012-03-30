using System;
using System.Web.Mvc;
using NHibernate;
using Spinit.Wpc.Synologen.Data.Commands;
using Spinit.Wpc.Synologen.Data.Queries;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
    	protected ISession _session { get; set; }
		public Action<Command> ExecuteCommandOverride { get; set; }
		public Func<Command, object> ExecuteCommandWithResultOverride { get; set; }
		public Func<Query,object> QueryOverride { get; set; }

    	public BaseController(ISession session)
    	{
    		_session = session;
    	}

    	protected TResult Query<TResult>(Query<TResult> query)
		{
			if(QueryOverride != null) return (TResult) QueryOverride(query);
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
			if(ExecuteCommandWithResultOverride != null) return (TResult) ExecuteCommandWithResultOverride(command);
			command.Session = _session;
			command.Execute();
			return command.Result;
		}
    }
}