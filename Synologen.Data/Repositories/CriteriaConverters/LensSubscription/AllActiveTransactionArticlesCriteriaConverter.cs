using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.LensSubscription
{
	public class AllActiveTransactionArticlesCriteriaConverter : NHibernateActionCriteriaConverter<AllActiveTransactionArticlesCriteria, TransactionArticle>, IActionCriteria
	{
		public AllActiveTransactionArticlesCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllActiveTransactionArticlesCriteria source)
		{
			return Criteria
				.FilterEqual(x => x.Active, true);
		}
	}
}