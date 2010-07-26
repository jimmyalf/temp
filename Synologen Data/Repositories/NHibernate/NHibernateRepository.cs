using NHibernate;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public class NHibernateRepository<TModel> : NHibernateReadonlyRepository<TModel>, IRepository<TModel> where TModel : class
	{
		public NHibernateRepository(ISession session) : base(session) {}

		public virtual void Save(TModel entity)
		{
			Session.SaveOrUpdate(entity);
			Session.Flush();
		}

		public virtual void Delete(TModel entity)
		{
			Session.Delete(entity);
			Session.Flush();
		}
	}
}