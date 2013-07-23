using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories
{
    public class ArticleRepository : NHibernateRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ISession session) : base(session)
        {
        }
    }
}