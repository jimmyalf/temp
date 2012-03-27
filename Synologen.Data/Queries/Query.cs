using NHibernate;

namespace Spinit.Wpc.Synologen.Data.Queries
{
	public abstract class Query<TResult>
	{
		public abstract TResult Execute();
		public ISession Session { get; set; }
	}
}