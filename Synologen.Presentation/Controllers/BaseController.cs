using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Commands;
using Spinit.Wpc.Synologen.Data.Queries;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
    	private readonly IAdminSettingsService _adminSettingsService;
    	private ISession _session { get; set; }
		
    	public Action<LambdaExpression> SessionWithoutResultOverride { get; set; }
		public Func<LambdaExpression, Type, object> SessionWithResultOverride { get; set; }

		public Action<Command> ExecuteCommandOverride { get; set; }
		public Func<Command, Type, object> ExecuteCommandWithResultOverride { get; set; }

		public Func<Query, Type, object> QueryOverride { get; set; }
		protected int DefaultPageSize { get; set; }

    	public BaseController(ISession session, IAdminSettingsService adminSettingsService) : this(session)
    	{
    		_adminSettingsService = adminSettingsService;
    		DefaultPageSize = _adminSettingsService.GetDefaultPageSize();
    	}

    	public BaseController(ISession session)
    	{
    		_session = session;
    		DefaultPageSize = 40;
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
			_session.Flush();
		}

		protected TResult Execute<TResult>(Command<TResult> command)
		{
			if (ExecuteCommandWithResultOverride != null) return (TResult) ExecuteCommandWithResultOverride(command, typeof (TResult));
			command.Session = _session;
			command.Execute();
			_session.Flush();
			return command.Result;
		}

		protected TResult WithSession<TResult>(Expression<Func<ISession, TResult>> expression)
		{
			if(SessionWithResultOverride != null) return (TResult) SessionWithResultOverride(expression, typeof(TResult));
			var function = expression.Compile();
			var returnValue =  function(_session);
			_session.Flush();
			return returnValue;
		}

		protected void WithSession(Expression<Action<ISession>> expression)
		{
			if(SessionWithoutResultOverride != null) SessionWithoutResultOverride(expression);
			var action = expression.Compile();
			action(_session);
			_session.Flush();
		}

		protected PagedSortedQuery<TType> GetPagedSortedQuery<TType>(GridPageSortParameters parameters, Func<ICriteria<TType>,ICriteria> additionalCriterias = null, int? pageSize = null) where TType : class
		{
			var thisPageSize = pageSize ?? DefaultPageSize;
			return new PagedSortedQuery<TType>(
				parameters.Page,
				parameters.PageSize ?? thisPageSize,
				parameters.Column,
				parameters.Direction == SortDirection.Ascending)
			{
				CustomCriteria = additionalCriterias
			};
		}
    }
}