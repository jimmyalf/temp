using NHibernate;
using Spinit.Wpc.Synologen.Data.Commands;
using Spinit.Wpc.Synologen.Data.Queries;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public abstract class DeviationPresenter<TView> : Presenter<TView>
		where TView : class, IView
	{
		public ISession Session { get; set; }

		protected DeviationPresenter(TView view, ISession session) : base(view)
		{
			Session = session;
		}

		protected TResult Query<TResult>(Query<TResult> query)
		{
			query.Session = Session;
			return query.Execute();
		}

		protected void Execute(Command command)
		{
			command.Session = Session;
			command.Execute();
			Session.Flush();
		}

		protected TResult Execute<TResult>(Command<TResult> command)
		{
			command.Session = Session;
			command.Execute();
			Session.Flush();
			return command.Result;
		}
	}
}