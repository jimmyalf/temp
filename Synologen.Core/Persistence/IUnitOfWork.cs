namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface IUnitOfWork
	{
		void Dispose();
		void Commit();
		void Rollback();
	}

	public interface IUnitOfWork<TSession> : IUnitOfWork
	{
		TSession Session { get; }
	}
}