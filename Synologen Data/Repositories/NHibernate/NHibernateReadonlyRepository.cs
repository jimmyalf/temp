using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public class NHibernateReadonlyRepository<TModel> : IReadonlyRepository<TModel> where TModel : class
	{
		public NHibernateReadonlyRepository(ISession session)
		{
			Session = session;
		}

		protected ISession Session { get; private set; }

		public virtual TModel Get(int id)
		{
			return Session.Get<TModel>(id);
		}

		public virtual IEnumerable<TModel> GetAll()
		{
			return Session.CreateCriteria<TModel>().List<TModel>();
		}

		public virtual IEnumerable<TModel> FindBy<TActionCriteria>(TActionCriteria actionCriteria) where TActionCriteria : IActionCriteria
		{
			if (actionCriteria is IPagedCriteria)
			{
				long count;
				var pagedCriteria = (IPagedCriteria) actionCriteria;
				var result = GetPagedResult(actionCriteria, out count);
				return new PagedList<TModel>(result, count, pagedCriteria.Page, pagedCriteria.PageSize);
			}
			return actionCriteria.Convert<TActionCriteria, ICriteria>().List<TModel>();
		}

		private IEnumerable<TModel> GetPagedResult<TPagedActionCriteria>(TPagedActionCriteria pagedActionCriteria, out long count) where TPagedActionCriteria : IActionCriteria
		{
				var multiResults = Session.CreateMultiCriteria()
					 .Add(pagedActionCriteria.Convert<TPagedActionCriteria, ICriteria>())
					 .Add(pagedActionCriteria.Convert<TPagedActionCriteria, ICriteria>()
						.SetFirstResult(0)
						.SetMaxResults(-1)
						.SetProjection(Projections.RowCountInt64())
						).List();
				count = (long)((IList)multiResults[1])[0];
				return (IEnumerable<TModel>) ((ArrayList)multiResults[0]).ToArray(typeof(TModel));
		}

		//public virtual IEnumerable<TModel> FindBy<TActionCriteria>(TActionCriteria actionCriteria) where TActionCriteria : IActionCriteria
		//{
		//    if (actionCriteria is IPagedCriteria)
		//    {
		//        var pagedCriteria = (IPagedCriteria) actionCriteria;
		//        var result = actionCriteria.Convert<TActionCriteria, ICriteria>().List<TModel>();
		//        var count = actionCriteria.Convert<TActionCriteria, ICriteria>()
		//            .SetFirstResult(0)
		//            .SetMaxResults(-1)
		//            .SetProjection(Projections.RowCountInt64())
		//            .UniqueResult<long>();
		//        return new PagedList<TModel>(result, count, pagedCriteria.Page, pagedCriteria.PageSize);
		//    }

		//    return actionCriteria.Convert<TActionCriteria, ICriteria>().List<TModel>();
		//}
	}
}