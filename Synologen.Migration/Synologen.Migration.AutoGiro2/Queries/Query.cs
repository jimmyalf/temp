using NHibernate;

namespace Synologen.Migration.AutoGiro2.Queries
{
	public abstract class Query<TResult>
	{
		public abstract TResult Execute();
		public ISession Session { get; set; }
	}
}