using NHibernate;
using Synologen.Migration.AutoGiro2.Queries;

namespace Synologen.Migration.AutoGiro2.Commands
{
	public abstract class Command
	{
		public abstract void Execute();
		public ISession Session { get; set; }

		protected TResult Query<TResult>(Query<TResult> query)
		{
			query.Session = Session;
			return query.Execute();
		}
	}

	public abstract class Command<TResult> : Command
	{
		public TResult Result { get; protected set; }
	}
}