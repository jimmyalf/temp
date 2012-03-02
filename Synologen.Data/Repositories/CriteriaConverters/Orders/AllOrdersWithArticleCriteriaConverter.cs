using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Order = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Order;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllOrdersWithArticleCriteriaConverter : NHibernateActionCriteriaConverter<AllOrdersWithArticleCriteria,Order>
	{
		public AllOrdersWithArticleCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllOrdersWithArticleCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Article.Left)
				.CreateAlias(x => x.Article.Right)
				.Add(Restrictions.Or(
					Restrictions.Eq(Property(x => x.Article.Left.Id), source.ArticleId),
					Restrictions.Eq(Property(x => x.Article.Right.Id), source.ArticleId)
				));
		}
	}
}