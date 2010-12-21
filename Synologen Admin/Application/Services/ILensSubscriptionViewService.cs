using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface ILensSubscriptionViewService 
	{
		IEnumerable<SubscriptionListItemView> GetSubscriptions(PageOfSubscriptionsMatchingCriteria criteria);
		SubscriptionView GetSubscription(int subscriptionId);
		IEnumerable<TransactionArticleListItem> GetTransactionArticles(PageOfTransactionArticlesMatchingCriteria criteria);
		TransactionArticleModel GetTransactionArticle(int id);
		void SaveTransactionArticle(TransactionArticleModel model);
		void DeleteTransactionArticle(int id);
	}
}