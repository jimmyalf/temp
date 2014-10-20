using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders
{
	public class AllArticleTypesWithCategoryCriteriaConverter : NHibernateActionCriteriaConverter<AllArticleTypesWithCategoryCriteria,ArticleType>
	{
		public AllArticleTypesWithCategoryCriteriaConverter(ISession session) : base(session) {}
		public override ICriteria Convert(AllArticleTypesWithCategoryCriteria source)
		{
			return Criteria
				.CreateAlias(x => x.Category)
				.FilterEqual(x => x.Category.Id, source.ArticleCategoryId);
		}
	}
}