using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories
{
	public class ArticleRepository : NHibernateReadonlyRepository<Article>, IArticleRepository
	{
		public ArticleRepository(ISession session) : base(session) {}
	}
}