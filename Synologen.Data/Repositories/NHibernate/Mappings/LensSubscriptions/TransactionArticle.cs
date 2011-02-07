using FluentNHibernate.Mapping;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.LensSubscriptions
{
	public class TransactionArticleMap : ClassMap<TransactionArticle>
	{
		public TransactionArticleMap()
		{
			Table("SynologenLensSubscriptionTransactionArticle");
			Id(x => x.Id);
			Map(x => x.Name).Not.Nullable();
			Map(x => x.Active).Not.Nullable();
			Map(x => x.NumberOfConnectedTransactions)
				.Formula("(Select Count('') from SynologenLensSubscriptionTransaction Where SynologenLensSubscriptionTransaction.ArticleId = Id)");
		}
	}
}