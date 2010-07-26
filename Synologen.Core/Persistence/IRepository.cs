namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface IRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : class
	{
		void Save(TEntity entity);
		void Delete(TEntity entity);
	}
}