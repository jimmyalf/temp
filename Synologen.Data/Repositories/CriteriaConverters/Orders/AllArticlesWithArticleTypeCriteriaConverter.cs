using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllArticlesWithArticleTypeCriteriaConverter : NHibernateActionCriteriaConverter<AllArticlesWithArticleTypeCriteria,Article>
	{
		public AllArticlesWithArticleTypeCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllArticlesWithArticleTypeCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.ArticleType)
				.FilterEqual(x => x.ArticleType.Id, source.ArticleTypeId);
		}
	}
}