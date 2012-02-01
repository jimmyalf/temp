using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllOrdersWithArticleCriteriaConverter : NHibernateActionCriteriaConverter<AllOrdersWithArticleCriteria,Order>
	{
		public AllOrdersWithArticleCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllOrdersWithArticleCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Article)
				.FilterEqual(x => x.Article.Id, source.ArticleId);
		}
	}
}