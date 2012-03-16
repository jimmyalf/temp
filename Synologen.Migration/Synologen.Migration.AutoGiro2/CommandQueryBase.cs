using NHibernate;
using Simple.Data;
using Synologen.Migration.AutoGiro2.Commands;
using Synologen.Migration.AutoGiro2.Queries;

namespace Synologen.Migration.AutoGiro2
{
	public abstract class CommandQueryBase
	{
		public dynamic DB { get; set; }
		protected ISession Session;

		protected CommandQueryBase() { }
		protected CommandQueryBase(ISession session)
		{
			DB = Database.OpenNamedConnection("WpcServer");;
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
			command.DB = DB;
			command.Execute();
		}

		protected TResult Execute<TResult>(Command<TResult> command)
		{
			command.Session = Session;
			command.DB = DB;
			command.Execute();
			return command.Result;
		}
	}
}