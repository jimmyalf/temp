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
			//TODO: This critiera might not work (needs to be checked manually)
			return Criteria
				.CreateAlias(x => x.LensRecipe.Article.Left)
				.CreateAlias(x => x.LensRecipe.Article.Right)
				.Add(Restrictions.Or(
					Restrictions.Eq(Property(x => x.LensRecipe.Article.Left.Id), source.ArticleId),
					Restrictions.Eq(Property(x => x.LensRecipe.Article.Right.Id), source.ArticleId)
				));
		}
	}
}