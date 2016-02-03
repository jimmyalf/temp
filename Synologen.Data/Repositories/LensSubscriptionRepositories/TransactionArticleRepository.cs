using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories
{
	public class TransactionArticleRepository :  NHibernateRepository<TransactionArticle>, ITransactionArticleRepository
	{
		public TransactionArticleRepository(ISession session) : base(session) {}
	}
}